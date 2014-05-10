using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Archer : Unit
{
	public new void Awake()
	{
		base.Awake();
		
		mDefaultHealth = 4;
		
		mRange = 40f; // sword range
		mMaxReloadTime = 2.5f;
		mMinReloadTime = 3.75f;
		mAccuracy = 0.5f;
		
		mMovementSpeed = 8f; // units per second
		mChargeSpeed = 10f;   // speed used to engage enemies
		
		mAllegiance = Allegiance.kRodelle;
	}

	
	
	void Start()
	{
		mWeapons = new SortedList();
		
		GameObject crossbow = Resources.Load("Squads/Prefabs/CrossbowPrefab") as GameObject;
		GameObject o = (GameObject) Instantiate(crossbow);
		Crossbow w = (Crossbow) o.GetComponent(typeof(Crossbow));

		mWeapons.Add (w.Range, w);
		
		
		mHealth = mDefaultHealth;
		mPreviousHealth = mHealth;
		mSprites = null;
	}
}
