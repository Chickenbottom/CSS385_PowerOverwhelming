using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeBehavior : Target {

	public float Range { get { return kRange; } }
	private const float kRange = 10f; // sword range
	
	private const float kMaxChargeTime = .5f;
	private const float kMinChargeTime = .75f;
	private float mChargeTimer;
	
	private const float kAccuracy = 0.5f;
	
	private Target mAttackTarget;
	
	private Vector3 mFiringPosition;
	private Vector3 mDestination;
	
	private float mSpeed = 20f;
	private float mChargeSpeed = 35f;
	
	
	public MeleeSquad Squad { get; set; }
	
	protected enum MovementState {
		kMoving, kIdle
	}
	
	protected enum AttackState {
		kIdle, kEngaging, kShooting, kMelee
	}
	
	private MovementState mMovementState;
	private AttackState mAttackState;
	
	private GameObject mArrowPrefab = null;
	private List<Sprite> mSprites;
	
	void Start()
	{
		if (mArrowPrefab == null)
			mArrowPrefab = Resources.Load("ArcherDamage/Arrow") as GameObject;
			
		mHealth = kDefaultHealth;
		mPreviousHealth = mHealth;
		
		mSprites = new List<Sprite>();
		mSprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman3", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman2", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman1", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman", typeof(Sprite)) as Sprite);
	}
	
	void Awake ()
	{
		mChargeTimer = kMinChargeTime;
		
		mAttackState = AttackState.kIdle;
		mAttackTarget = null;
		
		mMovementState = MovementState.kIdle;
		mDestination = new Vector3(0, 0, 0);
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
		
		// TODO replace with EnemyUnit base type
		if (other.gameObject.name.Equals("MeleeEnemy") ||
		    other.gameObject.name.Equals("MeleeEnemy(Clone)")) {
			
			if (this.Squad == null)
				Debug.Log ("Squad not initialized for squad member");
			
			this.Squad.NotifyEnemySighted(this, other.gameObject);
			Debug.Log ("Enemy sighted!");
		}
	}
	
	public void ShootAt(MeleeEnemyBehavior enemyUnit, Vector3? firingPosition = null)
	{
		mFiringPosition = (firingPosition != null) 
			? firingPosition.Value
				: this.transform.position;
		mFiringPosition = this.transform.position; // melee units don't need a firing position
		
		mMovementState = MovementState.kIdle;
		mAttackTarget = enemyUnit;
		
		mAttackState = AttackState.kEngaging;
	}
	
	// Update is called once per frame
	public void FixedUpdate () 
	{
		switch (mAttackState)
		{
		case (AttackState.kIdle):
			break;
			
		case (AttackState.kEngaging):
			EngageTarget(mAttackTarget);
			break;
			
		case (AttackState.kShooting):
			UpdateAttack(mAttackTarget);
			break;
		}
		
		switch (mMovementState)
		{
		case (MovementState.kIdle):
			break;
			
		case (MovementState.kMoving):
			UpdateMovement(mDestination, mSpeed);
			break;
		}			
	}
	
	private void EngageTarget(Target target)
	{
		if (target == null) {
			return;
		}
		
		// if in range, start firing!
		Vector3 targetLocation = target.transform.position;
		if (Vector3.Distance(this.transform.position, targetLocation) <= kRange) {
			mAttackState = AttackState.kShooting;
			return;
		}
		
		
		// First priority is to move into the firing position
		if (Vector3.Distance(this.transform.position, mFiringPosition) > 1.0f) {
			//Debug.Log ("Moving into position");
			UpdateMovement(mFiringPosition, mChargeSpeed);
		} else {
			mAttackState = AttackState.kShooting;
		}
		
	}
	
	private void UpdateAttack(Target target)
	{
		if (target == null) { // target's been destroyed
			mAttackState = AttackState.kIdle;
			mMovementState = MovementState.kMoving;
			this.Squad.NotifyEnemyKilled(this);
			return;
		}
		
		// Move into firing range
		Vector3 targetLocation = target.transform.position;
		if (Vector3.Distance(this.transform.position, targetLocation) > kRange) {
			//Debug.Log ("Moving into range");
			UpdateMovement(targetLocation, mChargeSpeed);
			return;
		}
		
		mChargeTimer -= Time.deltaTime;
		if (mChargeTimer < 0) {
			FireArrow(target);
		}
		//Debug.Log ("In Range, firing!");
	}
	
	private void FireArrow(Target target)
	{
		if (target == null)
			return;
		
		mChargeTimer = Random.Range (kMinChargeTime, kMaxChargeTime);
		
		GameObject o = (GameObject) Instantiate(mArrowPrefab);
		o.transform.position = transform.position;
		
		ArrowBehavior a = (ArrowBehavior) o.GetComponent(typeof(ArrowBehavior));
		a.SetDestination(target.transform.position);
		
		MeleeEnemyBehavior e = (MeleeEnemyBehavior) target.GetComponent(typeof(MeleeEnemyBehavior));
		e.Damage(1);
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
	}
	
	/////////////////////////////////////////////////////////////
	// Damage Model
	/////////////////////////////////////////////////////////////
	private int kDefaultHealth = 4;
	private int mHealth;
	private int mPreviousHealth;
	
	void Update() 
	{
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
}
