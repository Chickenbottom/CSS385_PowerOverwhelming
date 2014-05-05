using UnityEngine;
using System.Collections;

public class ArcherUnit : MonoBehaviour 
{
	public float Range { get { return kRange; } }
	private const float kRange = 45f;
	
	private const float kChargeTime = 1.0f;
	private float mChargeTimer;
	
	private const float kAccuracy = 0.5f;
	
	private GameObject mAttackTarget;
	
	private Vector3 mFiringPosition;
	private Vector3 mDestination;
	
	private float mSpeed = 20f;
	private float mChargeSpeed = 30f;
	
	
	public ArcherSquad Squad { get; set; }
	
	protected enum MovementState {
		kMoving, kIdle
	}
	
	protected enum AttackState {
		kIdle, kEngaging, kShooting, kMelee
	}
	
	private MovementState mMovementState;
	private AttackState mAttackState;
	
	void Awake ()
	{
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
	
	// Check for enemies in sight range
	void OnTriggerEnter2D(Collider2D other)
	{
		if (this.collider2D.enabled == false)
			return;
			
		// TODO replace with EnemyUnit base type
		if (other.gameObject.name == "ArcherEnemy") {
		
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
		// First priority is to move into the firing position
		if (Vector3.Distance(this.transform.position, mFiringPosition) > 1.0f) {
			//Debug.Log ("Moving into position");
			UpdateMovement(mFiringPosition, mChargeSpeed);
		} else {
			mAttackState = AttackState.kShooting;
			UpdateAttack(target);
		}
	}
	
	private void UpdateAttack(GameObject target)
	{
		// Move into firing range
		Vector3 targetLocation = target.transform.position;
		if (Vector3.Distance(this.transform.position, targetLocation) > kRange) {
			//Debug.Log ("Moving into range");
			UpdateMovement(targetLocation, mChargeSpeed);
			return;
		}
		
		//Debug.Log ("In Range, firing!");
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
