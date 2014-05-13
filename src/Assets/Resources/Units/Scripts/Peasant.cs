﻿using UnityEngine;
using System.Collections;

public class Peasant : Unit 
{
	protected new void Awake()
	{
		base.Awake();
	
		mDefaultHealth = 5;
		
		mMovementSpeed = 5f; // units per second
		mChargeSpeed = 5f;   // speed used to engage enemies
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
