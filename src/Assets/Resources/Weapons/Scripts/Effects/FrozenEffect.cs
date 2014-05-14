using UnityEngine;
using System.Collections;

public class FrozenEffect : MonoBehaviour 
{
	public Target target; // TODO generalize to target
	
	
	private float mDuration = 5f;
	private float timer;
	
	public void Update()
	{
		if (Time.time - mStartTimer > mDuration)
			Unfreeze();
	}
	
	float mStartTimer;
	float originalCharge;
	float originalMovement;
	public void Freeze() {
		mStartTimer = Time.time;

		Unit u = target.GetComponent<Unit> ();
		if (u != null) {
			originalCharge = u.mChargeSpeed;
			originalMovement = u.mMovementSpeed;

			u.mChargeSpeed = 0f;
			u.mMovementSpeed = 0f;
		}	
	}
	
	private void Unfreeze()
	{
		Unit u = target.GetComponent<Unit> ();
		if (u != null) {
			u.mChargeSpeed = originalCharge;
			u.mMovementSpeed = originalMovement;
		}
		Destroy(this);
	}
}
