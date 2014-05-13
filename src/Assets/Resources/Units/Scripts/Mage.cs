﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mage : Unit
{
	protected new void Awake()
	{
		base.Awake();
		
		mDefaultHealth = 4;
		
		mMovementSpeed = 8f; // units per second
		mChargeSpeed = 10f;   // speed used to engage enemies
	}
	
	void Start()
	{
		mWeapons = new SortedList();
		
		Weapon dagger = new Dagger();
		mWeapons.Add (dagger.Range, dagger);
		mCurrentWeapon = dagger;
		
		mHealth = mDefaultHealth;
		mPreviousHealth = mHealth;
		mSprites = null;
	}
}
