using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swordsman : Unit
{
	public new void Awake()
	{
		base.Awake();
		mDefaultHealth = 4;
		
		mMovementSpeed = 10f; // units per second
		mChargeSpeed = 15f;   // speed used to engage enemies
		
		mAllegiance = Allegiance.kRodelle;
	}

	void Start()
	{
		if (mProjectilePrefab == null)
			mProjectilePrefab = Resources.Load("Squads/Prefabs/ArrowPrefab") as GameObject;
		
		mHealth = mDefaultHealth;
		mPreviousHealth = mHealth;
		
		mSprites = new List<Sprite>();
		mSprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman3", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman2", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman1", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman", typeof(Sprite)) as Sprite);
	}
}
