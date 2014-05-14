using UnityEngine;
using System.Collections;

public class King : Unit {

	protected new void Awake()
	{
		base.Awake();
		
		mSightRange = 5f;
		mDefaultHealth = 100;
		
		mMovementSpeed = 1f; // units per second
		mChargeSpeed = 1f;   // speed used to engage enemies
	}
	
	public void Start()
	{
		mWeapons = new SortedList();
		Weapon sword = new Sword();
		mCurrentWeapon = sword;
		mWeapons.Add (sword.Range, sword);
		
		mHealth = mDefaultHealth;
		mPreviousHealth = mHealth;
	}
}
