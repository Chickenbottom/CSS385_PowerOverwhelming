using UnityEngine;
using System.Collections;

public class Peasant : Unit 
{
	public new void Awake()
	{
		base.Awake();
	
		mDefaultHealth = 2;
		
		mRange = 10f; // sword range
		mMaxReloadTime = 5.5f;
		mMinReloadTime = 5.75f;
		mAccuracy = 0.5f;
		
		mMovementSpeed = 8f; // units per second
		mChargeSpeed = 10f;   // speed used to engage enemies
		
		mAllegiance = Allegiance.kAI;
	}
	
	void Start()
	{
		if (mProjectilePrefab == null)
			mProjectilePrefab = Resources.Load("Squads/Prefabs/ArrowPrefab") as GameObject;
		
		mHealth = mDefaultHealth;
		mPreviousHealth = mHealth;
		mSprites = null;
	}
}
