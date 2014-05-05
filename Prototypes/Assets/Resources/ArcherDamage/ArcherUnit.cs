using UnityEngine;
using System.Collections;

public class ArcherUnit : MonoBehaviour 
{
	public float Range { get { return kRange; } }
	private const float kRange = 65f;
	
	private const float kMaxChargeTime = .4f;
	private const float kMinChargeTime = 1.2f;
	private float mChargeTimer;
	
	private const float kAccuracy = 0.5f;
	
	private GameObject mAttackTarget;
	
	private Vector3 mFiringPosition;
	private Vector3 mDestination;
	
	private float mSpeed = 20f;
	private float mChargeSpeed = 20f;
	
	
	public ArcherSquad Squad { get; set; }
	
	protected enum MovementState {
		kMoving, kIdle
	}
	
	protected enum AttackState {
		kIdle, kEngaging, kShooting, kMelee
	}
	
	private MovementState mMovementState;
	private AttackState mAttackState;
	
	private GameObject mArrowPrefab = null;
	
	void Start()
	{
		if (mArrowPrefab == null)
			mArrowPrefab = Resources.Load("ArcherDamage/Arrow") as GameObject;
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
		if (other.gameObject.name.Equals("ArcherEnemy") ||
		    other.gameObject.name.Equals("ArcherEnemy(Clone)")) {
		
			if (this.Squad == null)
				Debug.Log ("Squad not initialized for squad member");
				
			this.Squad.NotifyEnemySighted(this, other.gameObject);
			Debug.Log ("Enemy sighted!");
		}
	}
	
	public void ShootAt(GameObject enemyUnit, Vector3? firingPosition = null)
	{
		mFiringPosition = (firingPosition != null) 
			? firingPosition.Value
			: this.transform.position;
			
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
	
	private void EngageTarget(GameObject target)
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
	
	private void UpdateAttack(GameObject target)
	{
		if (target == null) { // target's been destroyed
			mAttackState = AttackState.kIdle;
			mMovementState = MovementState.kMoving;
			this.collider2D.enabled = true;
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
	
	private void FireArrow(GameObject target)
	{
		if (target == null)
			return;
			
		mChargeTimer = Random.Range (kMinChargeTime, kMaxChargeTime);
		
		GameObject o = (GameObject) Instantiate(mArrowPrefab);
		o.transform.position = transform.position;
		
		ArrowBehavior a = (ArrowBehavior) o.GetComponent(typeof(ArrowBehavior));
		a.SetDestination(target.transform.position);
	
		ArcherEnemyBehavior e = (ArcherEnemyBehavior) target.GetComponent(typeof(ArcherEnemyBehavior));
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
}
