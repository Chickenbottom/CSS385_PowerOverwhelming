using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Target, IDamagable
{	
	#region Unit Stats
	protected int mDefaultHealth;
		
	protected float mMovementSpeed; // units per second
	protected float mChargeSpeed;   // speed used to engage enemies
	#endregion
	
	public Weapon CurrentWeapon = null;
	protected SortedList mWeapons;
	
	protected float mReloadTimer;
	protected Target mAttackTarget;
	
	protected Vector3 mAttackVector; // direction to attack target from
	protected Vector3 mDestination;  // location to move to when no other actions are taking place
	
	protected int mHealth;
	protected int mPreviousHealth;
	
	protected MovementState mMovementState;
	protected AttackState mAttackState;
	
	protected GameObject mProjectilePrefab = null;
	protected List<Sprite> mSprites;
	
	#region Getters and Setters
	public Squad Squad { get; set; }
	public float Range { get { return CurrentWeapon.Range; } }
	#endregion
	
	protected enum MovementState {
		kMoving, kIdle
	}
	
	protected enum AttackState {
		kIdle, kEngaging, kRanged, kMelee
	}
	
	public void MoveTo(Vector3 target) 
	{
		mDestination = target;
		
		if (mAttackTarget == null) // can only move if there is no one in range
			mMovementState = MovementState.kMoving;
	}
	
	
	void OnTriggerStay2D(Collider2D other)
	{
		this.OnTriggerEnter2D(other);
	}
	
	// Check for enemies in sight range
	void OnTriggerEnter2D(Collider2D other)
	{
		if (this.collider2D.enabled == false)
			return;
		
		Target t = other.gameObject.GetComponent<Target>();
		if (t != null && t.GetAllegiance != this.mAllegiance) {			
			if (this.Squad == null)
				Debug.Log ("Squad not initialized for squad member");
			
			this.Squad.NotifyEnemySighted(this, other.gameObject);
		}
	}
	
	public void Engage(Target target, Vector3? firingPosition = null)
	{
		mAttackVector = (firingPosition != null) 
			? firingPosition.Value
			: this.transform.position;
		
		mMovementState = MovementState.kIdle;
		mAttackTarget = target;
		
		mAttackState = AttackState.kEngaging;
	}
	
	// Update is called once per frame
	public void FixedUpdate () 
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
	
	public void Disengage()
	{
		mAttackState = AttackState.kIdle;
		mMovementState = MovementState.kMoving;
		
		CurrentWeapon = (Weapon)mWeapons.GetByIndex(mWeapons.Count - 1); // longest range weapon
	}	
	
	private void UpdateTargetState()
	{
		if (mAttackState == AttackState.kIdle) // no target present
			return; 
			
		if (mAttackTarget == null) { // target's been destroyed
			CurrentWeapon.Reset();
			this.Squad.NotifyEnemyKilled(this);
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
		if (Vector3.Distance(this.transform.position, targetLocation) <= CurrentWeapon.Range ||
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
			
		// Move into range of the target
		Vector3 targetLocation = target.transform.position;
		if (Vector3.Distance(this.transform.position, targetLocation) > CurrentWeapon.Range) {
			mAttackState = AttackState.kEngaging;
			UpdateMovement(targetLocation, mChargeSpeed);
			return;
		}
		
		Weapon w = (Weapon) mWeapons.GetByIndex(0);
		if (w != null)
			w.Attack(this, target);
	}
	
	private void UpdateMovement(Vector3 targetLocation, float speed)
	{
		if (Vector3.Distance(this.transform.position, targetLocation) < 1.0f) {
			mMovementState = MovementState.kIdle;
			return;
		}
		
		Vector3 targetDir = targetLocation - transform.position;
		targetDir.Normalize();
		
		transform.position += speed * Time.deltaTime * targetDir;
		
		int sortingOrder = (int)(-transform.position.y + Camera.main.orthographicSize);
		GetComponent<SpriteRenderer> ().sortingOrder = (int)(sortingOrder);
	}
	
	/////////////////////////////////////////////////////////////
	// Damage Model
	/////////////////////////////////////////////////////////////

	void UpdateDamageAnimation() 
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
	
	public void Damage(int damage)
	{
		mHealth -= damage;
		if (mHealth <= 0) {
			this.Squad.NotifyUnitDied(this);
			Destroy(this.gameObject);
			Destroy (this);
		}
	}
	
	public void Awake ()
	{		
		mAttackState = AttackState.kIdle;
		mAttackTarget = null;
		
		mMovementState = MovementState.kIdle;
		mDestination = new Vector3(0, 0, 0);
	}
}
