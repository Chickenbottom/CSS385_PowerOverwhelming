using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Target
{   
    public Squad Squad { get; set; }

    public float Range { get { return mCurrentWeapon.Range; } }

    public bool IsIdle { get { return this.mMovementState == MovementState.Idle; } }

    public float SightRange = 39f;
    
    // TODO replace with unit stats dictionary
    public float MoveSpeed { // units per second
        get { return mMovementSpeed; } 
        set { mMovementSpeed = value; }
    }
    
    public float ChargeSpeed { // units per second
        get { return mChargeSpeed; } 
        set { mChargeSpeed = value; }
    } 
    
///////////////////////////////////////////////////////////////////////////////////
// Public Methods
///////////////////////////////////////////////////////////////////////////////////
    
    /// <summary>
    /// Updates the destination of the unit.
    /// If the unit is not engaged in combat, it will move towards this desination.
    /// </summary>
    /// <param name="target">Desination. The location to move towards. </param>
    public void MoveTo (Vector3 destination)
    {
        this.mDestination = destination;
        
        if (mAttackTarget == null) // can only move if there is no one in range
            mMovementState = MovementState.Moving;
    }
        
    /// <summary>
    /// Engage the specified target from the given firingPosition.
    /// Units will attempt to move towards the firingPosition before attacking enemies.
    /// </summary>
    /// <param name="target">Target. The target to attack.</param>
    /// <param name="firingPosition">Firing position. The position to attack from.</param>
    public void Engage (Target target, Vector3? firingPosition = null)
    {
        mAttackVector = (firingPosition != null) 
            ? firingPosition.Value
            : this.Position;
        
        mMovementState = MovementState.Idle;
        mAttackTarget = target;
        
        mAttackState = AttackState.Engaging;
    }
    
    /// <summary>
    /// Reduces the health of the unit by the given amount.
    /// </summary>
    /// <param name="damage">Damage. The amount of damage taken.</param>
    public override void Damage (int damage)
    {
        mHealth -= damage;
        if (mHealth <= 0) {
            this.Squad.Notify (SquadAction.UnitDied, this);
            Destroy (this.gameObject);
        }
    }
    
    /// <summary>
    /// Switchs to weapon stored at the given index. If the index refers to an invalid weapon, no action is taken.
    /// </summary>
    /// <param name="index">Index.</param>
    public void SwitchToWeapon (int index)
    {
        Weapon w = (Weapon)mWeapons.GetByIndex (index);
        
        if (w != null) {
            mCurrentWeapon.Reset ();
            mCurrentWeapon = w;
        }
    }
    
    /// <summary>
    /// Disenganges the unit from combat and starts them moving towards their original destination.
    /// The unit switches to their longest ranged weapon.
    /// </summary>
    public void Disengage ()
    {
        mAttackState = AttackState.Idle;
        mMovementState = MovementState.Moving;
        mAttackTarget = null;
        
        SwitchToWeapon (mWeapons.Count - 1); // longest range weapon
    }   
    
///////////////////////////////////////////////////////////////////////////////////
// Private Methods
///////////////////////////////////////////////////////////////////////////////////
    
    protected enum MovementState
    {
        Moving,
        Idle
    }
    
    protected enum AttackState
    {
        Idle,
        Engaging,
        Ranged,
        Melee
    }
    
    protected UnitAnimation mUnitAnimator;
    protected int mDefaultHealth;
    protected float mMovementSpeed; // units per second
    protected float mChargeSpeed;   // speed used to engage enemies
    
    protected Weapon mCurrentWeapon = null;
    protected SortedList mWeapons;
    protected Target mAttackTarget;
    protected Vector3 mAttackVector; // direction to attack target from
    protected Vector3 mDestination;  // location to move to when no other actions are taking place
    
    protected MovementState mMovementState;
    protected AttackState mAttackState;
    
    private void UpdateTargetState ()
    {
        if (mAttackState == AttackState.Idle) // no target present
            return; 
            
        if (mAttackTarget == null) { // target's been destroyed
            mCurrentWeapon.Reset ();
            this.Squad.Notify (SquadAction.UnitDestroyed, this);
            return;
        }
        
        if (mAttackTarget.Allegiance == mAllegiance) {
            this.Squad.Notify (SquadAction.TargetDestroyed);
            return;
        }
    }
    
    private void EngageTarget (Target target)
    {
        if (target == null)
            return;
        
        Vector3 targetLocation = target.Position;   
        Vector3 firingPosition = targetLocation + mAttackVector;
        
        float targetDistance = Vector3.SqrMagnitude (targetLocation - this.Position);
        float firingPositionDistance = Vector3.SqrMagnitude (targetLocation - firingPosition);
        
        // if in range, start firing!
        // do not move away from target
        if (Vector3.Distance (this.Position, targetLocation) <= mCurrentWeapon.Range ||
            firingPositionDistance > targetDistance) {
            mAttackState = AttackState.Ranged;
            UpdateAttack (target);
            return;
        }
        
        // Second priority is to move into the firing position
        if (Vector3.Distance (this.Position, firingPosition) > 1.0f) {
            //Debug.Log ("Moving into position");
            UpdateMovement (firingPosition, mChargeSpeed);
        } else { // firing position reached, attack!
            mAttackState = AttackState.Ranged;
            UpdateAttack (target);
        }
    }
    
    private void UpdateAttack (Target target)
    {
        if (target == null)
            return;
        
        Vector3 targetLocation = target.Position;
        float targetDistanceSquared = Vector3.SqrMagnitude (targetLocation - this.Position);
        
        for (int i = 0; i < mWeapons.Count; ++i) {
            Weapon w = (Weapon)mWeapons.GetByIndex (i);
            if (w == mCurrentWeapon) // can only check longer range weapons from here
                break; 
                
            // a shorter range weapon can be used
            if (w.Range * w.Range > targetDistanceSquared) { 
                Squad.Notify (SquadAction.WeaponChanged, this, i); // switch squad to this weapon
                break;
            }
        }
            
        // Move into range of the target
        if (targetDistanceSquared > mCurrentWeapon.Range * mCurrentWeapon.Range) {
            mAttackState = AttackState.Engaging;
            UpdateMovement (targetLocation, mChargeSpeed);
            return;
        }
        
        if (mCurrentWeapon != null)
            mCurrentWeapon.Attack (this, target);
    }
    
    private void UpdateMovement (Vector3 targetLocation, float speed)
    {
        if (Vector3.SqrMagnitude (this.Position - targetLocation) < 1.0f) {
            mMovementState = MovementState.Idle;
            this.Squad.Notify (SquadAction.DestinationReached);
            return;
        }
        
        Vector3 targetDir = targetLocation - Position;
        targetDir.Normalize ();
            
        Position += speed * Time.deltaTime * targetDir;
        
        
        int sortingOrder = (int)(-Position.y + Camera.main.orthographicSize);
        GetComponent<SpriteRenderer> ().sortingOrder = (int)(sortingOrder);
    }
    
    // Update sprite set to match current health
    private void UpdateAnimation ()
    {
        if (mAttackState == AttackState.Idle && mMovementState == MovementState.Idle) {
            mUnitAnimator.Idle ();
            return;
        }
        
        Vector3 direction;
        if (mAttackTarget != null)
            direction = mAttackTarget.Position - this.Position;
        else 
            direction = mDestination - this.Position;
            
        if (direction.x > 0)
            mUnitAnimator.WalkRight ();
        else 
            mUnitAnimator.WalkLeft ();
    }
    
///////////////////////////////////////////////////////////////////////////////////
// Unity Overrides
///////////////////////////////////////////////////////////////////////////////////
    
    // Update is called once per frame
    void FixedUpdate ()
    {
        UpdateTargetState ();
        
        switch (mAttackState) {
        case (AttackState.Idle):
            break;
            
        case (AttackState.Engaging):
            EngageTarget (mAttackTarget);
            break;
            
        case (AttackState.Ranged):
            UpdateAttack (mAttackTarget);
            break;
        }
        
        switch (mMovementState) {
        case (MovementState.Idle):
            break;
            
        case (MovementState.Moving):
            UpdateMovement (mDestination, mMovementSpeed);
            break;
        }
        
        UpdateAnimation ();
    }
    
    // Initialize variables
    protected void Awake ()
    {
        mAttackState = AttackState.Idle;
        mAttackTarget = null;
        
        mMovementState = MovementState.Idle;
        mDestination = new Vector3 (0, 0, 0);
        
        mUnitAnimator = this.GetComponent<UnitAnimation> ();
        if (mUnitAnimator == null)
            Debug.LogError ("UnitAnimation script was not attached to this Unit!");
    }
}
