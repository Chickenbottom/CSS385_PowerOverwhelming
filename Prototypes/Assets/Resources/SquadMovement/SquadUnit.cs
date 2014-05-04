using UnityEngine;
using System.Collections;

public class SquadUnit : MonoBehaviour {

	private Vector3 mTarget;
	private float mSpeed = 20f;	
	
	protected enum MovementState
	{
		kMoving, kIdle
	}
	
	private MovementState mMovementState;
		
	void Awake ()
	{
		mMovementState = MovementState.kIdle;
		mTarget = new Vector3(0, 0, 0);
	}
	
	public void MoveTo(Vector3 target) 
	{
		mTarget = target;		
		mMovementState = MovementState.kMoving;
	}
	
	public void ShootAt(Vector3 target)
	{
	
	}
	
	// Update is called once per frame
	public void FixedUpdate () 
	{
		switch (mMovementState)
		{
		case (MovementState.kIdle):
			break;

		case (MovementState.kMoving):
			UpdateMovement();
			break;
		}			
	}
		
	public void UpdateMovement()
	{
		if (Vector3.Distance(this.transform.position, mTarget) < 1.0f) {
			mMovementState = MovementState.kIdle;
			return;
		}
	
		Vector3 targetDir = mTarget - transform.position;
		targetDir.Normalize();
		
		transform.position += mSpeed * Time.deltaTime * targetDir;
	}
}
