using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Archer : Unit
{
	protected new void Awake()
	{
		base.Awake();
		
		mDefaultHealth = 4;
		
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
		
		GameObject dagger = Resources.Load("Squads/Prefabs/DaggerPrefab") as GameObject;
		GameObject t = (GameObject) Instantiate(dagger);
		Dagger d = (Dagger) t.GetComponent(typeof(Dagger));
		mWeapons.Add (d.Range, d);
		
		mCurrentWeapon = w;
		
		mHealth = mDefaultHealth;
		mPreviousHealth = mHealth;
		mSprites = null;
	}
}
