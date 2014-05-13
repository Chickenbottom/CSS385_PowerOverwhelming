using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Target, IDamagable
{	
	public Squad Squad { get; set; }
	public float Range { get { return mCurrentWeapon.Range; } }
	
	///////////////////////////////////////////////////////////////////////////////////
	// Public Methods
	///////////////////////////////////////////////////////////////////////////////////
	
	/// <summary>
	/// Updates the destination of the unit.
	/// If the unit is not engaged in combat, it will move towards this desination.
	/// </summary>
	/// <param name="target">Desination. The location to move towards. </param>
	public void MoveTo(Vector3 destination) 
	{
		mDestination = destination;
		
		if (mAttackTarget == null) // can only move if there is no one in range
			mMovementState = MovementState.kMoving;
	}
	
	/// <summary>
	/// Engage the specified target from the given firingPosition.
	/// Units will attempt to move towards the firingPosition before attacking enemies.
	/// </summary>
	/// <param name="target">Target. The target to attack.</param>
	/// <param name="firingPosition">Firing position. The position to attack from.</param>
	public void Engage(Target target, Vector3? firingPosition = null)
	{
		mAttackVector = (firingPosition != null) 
			? firingPosition.Value
			: this.transform.position;
		
		mMovementState = MovementState.kIdle;
		mAttackTarget = target;
		
		mAttackState = AttackState.kEngaging;
	}
	
	/// <summary>
	/// Reduces the health of the unit by the given amount.
	/// </summary>
	/// <param name="damage">Damage. The amount of damage taken.</param>
	public void Damage(int damage)
	{
		mHealth -= damage;
		if (mHealth <= 0) {
			this.Squad.Notify(SquadAction.kUnitDied, this);
			Destroy(this.gameObject);
		}
	}
	
	/// <summary>
	/// Switchs to weapon stored at the given index. If the index refers to an invalid weapon, no action is taken.
	/// </summary>
	/// <param name="index">Index.</param>
	public void SwitchToWeapon(int index)
	{
		Weapon w = (Weapon)mWeapons.GetByIndex(index);
		
		if (w != null) {
			mCurrentWeapon.Reset();
			mCurrentWeapon = w;
		}
	}
	
	/// <summary>
	/// Disenganges the unit from combat and starts them moving towrads their original destination.
	/// The unit will begin searching for new targets and will switch to their longest ranged weapon.
	/// </summary>
	public void Disengage()
	{
		mAttackState = AttackState.kIdle;
		mMovementState = MovementState.kMoving;
		mAttackTarget = null;
		
		SwitchToWeapon(mWeapons.Count - 1); // longest range weapon
	}	
	
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods
	///////////////////////////////////////////////////////////////////////////////////
	
	protected enum MovementState {
		kMoving, kIdle
	}
	
	protected enum AttackState {
		kIdle, kEngaging, kRanged, kMelee
	}
	
	protected int mDefaultHealth;
	
	protected float mMovementSpeed; // units per second
	protected float mChargeSpeed;   // speed used to engage enemies
	
	protected Weapon mCurrentWeapon = null;
	protected SortedList mWeapons;
	
	protected Target mAttackTarget;
	
	protected Vector3 mAttackVector; // direction to attack target from
	protected Vector3 mDestination;  // location to move to when no other actions are taking place
	
	protected int mHealth;
	protected int mPreviousHealth;
	
	protected MovementState mMovementState;
	protected AttackState mAttackState;
	
	protected GameObject mProjectilePrefab = null;
	protected List<Sprite> mSprites;
	
	private void UpdateTargetState()
	{
		if (mAttackState == AttackState.kIdle) // no target present
			return; 
			
		if (mAttackTarget == null) { // target's been destroyed
			mCurrentWeapon.Reset();
			this.Squad.Notify (SquadAction.kUnitDestroyed, this);
			return;
		}
		
		if (mAttackTarget.Allegiance == this.Squad.Allegiance) {
			this.Squad.Notify(SquadAction.kTargetDestroyed);
			return;
		}
			
	}
	
	private void EngageTarget(Target target)
	{
		if (target == null)
			return;
		
		Vector3 targetLocation = target.transform.position;	
		Vector3 firingPosition = targetLocation + mAttackVector;
		
		float targetDistance = Vector3.SqrMagnitude(targetLocation - this.transform.position);
		float firingPositionDistance = Vector3.SqrMagnitude(targetLocation - firingPosition);
		
		// if in range, start firing!
		// do not move away from target
		if (Vector3.Distance(this.transform.position, targetLocation) <= mCurrentWeapon.Range ||
		    firingPositionDistance > targetDistance) {
			mAttackState = AttackState.kRanged;
			UpdateAttack(target);
			return;
		}
		
		// Second priority is to move into the firing position
		if (Vector3.Distance(this.transform.position, firingPosition) > 1.0f) {
			//Debug.Log ("Moving into position");
			UpdateMovement(firingPosition, mChargeSpeed);
		} else { // firing position reached, attack!
			mAttackState = AttackState.kRanged;
			UpdateAttack(target);
		}
	}
	
	private void UpdateAttack(Target target)
	{
		if (target == null)
			return;
		
		Vector3 targetLocation = target.transform.position;
		float targetDistanceSquared = Vector3.SqrMagnitude(targetLocation - this.transform.position);
		
		for (int i = 0; i < mWeapons.Count; ++i) {
			Weapon w = (Weapon)mWeapons.GetByIndex(i);
			if (w == mCurrentWeapon) // can only check longer range weapons from here
				break; 
				
			// a shorter range weapon can be used
		    if (w.Range * w.Range > targetDistanceSquared) { 
				Squad.Notify(SquadAction.kWeaponChanged, this, i); // switch squad to this weapon
				break;
		    }
		}
			
		// Move into range of the target
		if (targetDistanceSquared > mCurrentWeapon.Range * mCurrentWeapon.Range) {
			mAttackState = AttackState.kEngaging;
			UpdateMovement(targetLocation, mChargeSpeed);
			return;
		}
		
		if (mCurrentWeapon != null)
			mCurrentWeapon.Attack(this, target);
	}
	
	private void UpdateMovement(Vector3 targetLocation, float speed)
	{
		if (Vector3.Distance(this.transform.position, targetLocation) < 1.0f) {
			mMovementState = MovementState.kIdle;
			this.Squad.Notify(SquadAction.kDestinationReached);
			return;
		}
		
		Vector3 targetDir = targetLocation - transform.position;
		targetDir.Normalize();
		
		transform.position += speed * Time.deltaTime * targetDir;
		
		int sortingOrder = (int)(-transform.position.y + Camera.main.orthographicSize);
		GetComponent<SpriteRenderer> ().sortingOrder = (int)(sortingOrder);
	}
	
	// Update sprite set to match current health
	private void UpdateDamageAnimation() 
	{
		if (mSprites == null)
			return;
			
		if (mHealth < 0)
			return;
		
		if (mHealth != mPreviousHealth) {
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			sr.sprite = mSprites[mHealth];
		}
		
		mPreviousHealth = mHealth;
	}
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	///////////////////////////////////////////////////////////////////////////////////
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		UpdateTargetState();
		
		switch (mAttackState)
		{
		case (AttackState.kIdle):
			break;
			
		case (AttackState.kEngaging):
			EngageTarget(mAttackTarget);
			break;
			
		case (AttackState.kRanged):
			UpdateAttack(mAttackTarget);
			break;
		}
		
		switch (mMovementState)
		{
		case (MovementState.kIdle):
			break;
			
		case (MovementState.kMoving):
			UpdateMovement(mDestination, mMovementSpeed);
			break;
		}
		
		UpdateDamageAnimation();
	}
	
	// Initialize variables
	protected void Awake ()
	{		
		mAttackState = AttackState.kIdle;
		mAttackTarget = null;
		
		mMovementState = MovementState.kIdle;
		mDestination = new Vector3(0, 0, 0);
	}
}
