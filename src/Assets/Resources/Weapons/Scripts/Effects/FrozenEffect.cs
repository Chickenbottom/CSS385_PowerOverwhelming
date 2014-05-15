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
			originalCharge = u.chargeSpeed;
			originalMovement = u.movementSpeed;

			u.chargeSpeed = 0f;
			u.movementSpeed = 0f;
		}	
	}
	
	private void Unfreeze()
	{
		Unit u = target.GetComponent<Unit> ();
		if (u != null) {
			u.chargeSpeed = originalCharge;
			u.movementSpeed = originalMovement;
		}
		Destroy(this);
	}
}
