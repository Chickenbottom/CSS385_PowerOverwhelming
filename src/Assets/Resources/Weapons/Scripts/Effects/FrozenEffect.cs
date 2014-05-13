using UnityEngine;
using System.Collections;

public class FrozenEffect : MonoBehaviour 
{
	public Unit unit; // TODO generalize to target
	
	
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
		
		originalCharge = unit.mChargeSpeed;
		originalMovement = unit.mMovementSpeed;
		
		unit.mChargeSpeed = 0f;
		unit.mMovementSpeed = 0f;
	}
	
	private void Unfreeze()
	{
		unit.mChargeSpeed = originalCharge;
		unit.mMovementSpeed = originalMovement;
		Destroy(this);
	}
}
