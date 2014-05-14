using UnityEngine;
using System.Collections;

public class King : Unit {

	protected new void Awake()
	{
		base.Awake();
		mDefaultHealth = 100;
		
		mMovementSpeed = 3f; // units per second
		mChargeSpeed = 3f;   // speed used to engage enemies
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
