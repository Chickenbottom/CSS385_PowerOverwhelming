using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Target
{	
	public Squad Squad { get; set; }
	public float Range { get { return currentWeapon.Range; } }
	public bool IsIdle { get { return this.movementState == MovementState.Idle; } }
	public float SightRange = 39f;
	
	// TODO replace with unit stats dictionary
	public float MoveSpeed { // units per second
		get { return movementSpeed; } 
		set { movementSpeed = value; }
	} 
	
	public float ChargeSpeed { // units per second
		get { return chargeSpeed; } 
		set { chargeSpeed = value; }
	} 
	
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
		this.destination = destination;
		
		if (attackTarget == null) // can only move if there is no one in range
			movementState = MovementState.Moving;
	}
		
	/// <summary>
	/// Engage the specified target from the given firingPosition.
	/// Units will attempt to move towards the firingPosition before attacking enemies.
	/// </summary>
	/// <param name="target">Target. The target to attack.</param>
	/// <param name="firingPosition">Firing position. The position to attack from.</param>
	public void Engage(Target target, Vector3? firingPosition = null)
	{
		attackVector = (firingPosition != null) 
			? firingPosition.Value
			: this.Position;
		
		movementState = MovementState.Idle;
		attackTarget = target;
		
		attackState = AttackState.Engaging;
	}
	
	/// <summary>
	/// Reduces the health of the unit by the given amount.
	/// </summary>
	/// <param name="damage">Damage. The amount of damage taken.</param>
	public override void Damage(int damage)
	{
		health -= damage;
		if (health <= 0) {
			this.Squad.Notify(SquadAction.UnitDied, this);
			Destroy(this.gameObject);
		}
	}
	
	/// <summary>
	/// Switchs to weapon stored at the given index. If the index refers to an invalid weapon, no action is taken.
	/// </summary>
	/// <param name="index">Index.</param>
	public void SwitchToWeapon(int index)
	{
		Weapon w = (Weapon)weapons.GetByIndex(index);
		
		if (w != null) {
			currentWeapon.Reset();
			currentWeapon = w;
		}
	}
	
	/// <summary>
	/// Disenganges the unit from combat and starts them moving towards their original destination.
	/// The unit switches to their longest ranged weapon.
	/// </summary>
	public void Disengage()
	{
		attackState = AttackState.Idle;
		movementState = MovementState.Moving;
		attackTarget = null;
		
		SwitchToWeapon(weapons.Count - 1); // longest range weapon
	}	
	
///////////////////////////////////////////////////////////////////////////////////
// Private Methods
///////////////////////////////////////////////////////////////////////////////////
	
	protected enum MovementState {
		Moving, Idle
	}
	
	protected enum AttackState {
		Idle, Engaging, Ranged, Melee
	}
	
	private UnitAnimation mUnitAnimator;
	protected int defaultHealth;
	
	protected float movementSpeed; // units per second
	protected float chargeSpeed;   // speed used to engage enemies
	
	protected Weapon currentWeapon = null;
	protected SortedList weapons;
	
	protected Target attackTarget;
	
	protected Vector3 attackVector; // direction to attack target from
	protected Vector3 destination;  // location to move to when no other actions are taking place
	
	protected int health;
	protected int previousHealth;
	
	protected MovementState movementState;
	protected AttackState attackState;
	
	protected GameObject projectilePrefab = null;
	protected List<Sprite> sprites;
	
	private void UpdateTargetState()
	{
		if (attackState == AttackState.Idle) // no target present
			return; 
			
		if (attackTarget == null) { // target's been destroyed
			currentWeapon.Reset();
			this.Squad.Notify (SquadAction.UnitDestroyed, this);
			return;
		}
		
		if (attackTarget.Allegiance == mAllegiance) {
			this.Squad.Notify(SquadAction.TargetDestroyed);
			return;
		}
	}
	
	private void EngageTarget(Target target)
	{
		if (target == null)
			return;
		
		Vector3 targetLocation = target.Position;	
		Vector3 firingPosition = targetLocation + attackVector;
		
		float targetDistance = Vector3.SqrMagnitude(targetLocation - this.Position);
		float firingPositionDistance = Vector3.SqrMagnitude(targetLocation - firingPosition);
		
		// if in range, start firing!
		// do not move away from target
		if (Vector3.Distance(this.Position, targetLocation) <= currentWeapon.Range ||
		    firingPositionDistance > targetDistance) {
			attackState = AttackState.Ranged;
			UpdateAttack(target);
			return;
		}
		
		// Second priority is to move into the firing position
		if (Vector3.Distance(this.Position, firingPosition) > 1.0f) {
			//Debug.Log ("Moving into position");
			UpdateMovement(firingPosition, chargeSpeed);
		} else { // firing position reached, attack!
			attackState = AttackState.Ranged;
			UpdateAttack(target);
		}
	}
	
	private void UpdateAttack(Target target)
	{
		if (target == null)
			return;
		
		Vector3 targetLocation = target.Position;
		float targetDistanceSquared = Vector3.SqrMagnitude(targetLocation - this.Position);
		
		for (int i = 0; i < weapons.Count; ++i) {
			Weapon w = (Weapon)weapons.GetByIndex(i);
			if (w == currentWeapon) // can only check longer range weapons from here
				break; 
				
			// a shorter range weapon can be used
		    if (w.Range * w.Range > targetDistanceSquared) { 
				Squad.Notify(SquadAction.WeaponChanged, this, i); // switch squad to this weapon
				break;
		    }
		}
			
		// Move into range of the target
		if (targetDistanceSquared > currentWeapon.Range * currentWeapon.Range) {
			attackState = AttackState.Engaging;
			UpdateMovement(targetLocation, chargeSpeed);
			return;
		}
		
		if (currentWeapon != null)
			currentWeapon.Attack(this, target);
	}
	
	private void UpdateMovement(Vector3 targetLocation, float speed)
	{
		if (Vector3.SqrMagnitude(this.Position - targetLocation) < 1.0f) {
			movementState = MovementState.Idle;
			this.Squad.Notify(SquadAction.DestinationReached);
			return;
		}
		
		Vector3 targetDir = targetLocation - Position;
		targetDir.Normalize();
			
		Position += speed * Time.deltaTime * targetDir;
		
		
		int sortingOrder = (int)(-Position.y + Camera.main.orthographicSize);
		GetComponent<SpriteRenderer> ().sortingOrder = (int)(sortingOrder);
	}
	
	// Update sprite set to match current health
	private void UpdateAnimation() 
	{
		if (attackState == AttackState.Idle && movementState == MovementState.Idle) {
			mUnitAnimator.Idle();
			return;
		}
		
		Vector3 direction;
		if (attackTarget != null)
			direction = attackTarget.Position - this.Position;
		else 
			direction = destination - this.Position;
			
		if (direction.x > 0)
			mUnitAnimator.WalkRight();
		else 
			mUnitAnimator.WalkLeft();
	}
	
///////////////////////////////////////////////////////////////////////////////////
// Unity Overrides
///////////////////////////////////////////////////////////////////////////////////
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		UpdateTargetState();
		
		switch (attackState)
		{
		case (AttackState.Idle):
			break;
			
		case (AttackState.Engaging):
			EngageTarget(attackTarget);
			break;
			
		case (AttackState.Ranged):
			UpdateAttack(attackTarget);
			break;
		}
		
		switch (movementState)
		{
		case (MovementState.Idle):
			break;
			
		case (MovementState.Moving):
			UpdateMovement(destination, movementSpeed);
			break;
		}
		
		UpdateAnimation();
	}
	
	// Initialize variables
	protected void Awake ()
	{
		attackState = AttackState.Idle;
		attackTarget = null;
		
		movementState = MovementState.Idle;
		destination = new Vector3(0, 0, 0);
		
		mUnitAnimator = this.GetComponent<UnitAnimation>();
		if (mUnitAnimator == null)
			Debug.LogError("UnitAnimation script was not attached to this Unit!");
	}
}
