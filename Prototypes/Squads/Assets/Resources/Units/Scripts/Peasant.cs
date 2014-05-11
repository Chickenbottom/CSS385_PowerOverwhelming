using UnityEngine;
using System.Collections;

public class Peasant : Unit 
{
	protected new void Awake()
	{
		base.Awake();
	
		mDefaultHealth = 5;
		
		mMovementSpeed = 8f; // units per second
		mChargeSpeed = 10f;   // speed used to engage enemies
		
		mAllegiance = Allegiance.kAI;
	}
	
	void Start()
	{	
		mWeapons = new SortedList();
			
		Weapon pitchfork = new Pitchfork();
		mWeapons.Add (pitchfork.Range, pitchfork);
		mCurrentWeapon = pitchfork;
		
		mHealth = mDefaultHealth;
		mPreviousHealth = mHealth;
		mSprites = null;
	}
}
