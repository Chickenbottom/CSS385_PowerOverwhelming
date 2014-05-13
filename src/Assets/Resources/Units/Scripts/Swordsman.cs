using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swordsman : Unit
{
	protected new void Awake()
	{
		base.Awake();
		mDefaultHealth = 4;
		
		mMovementSpeed = 10f; // units per second
		mChargeSpeed = 15f;   // speed used to engage enemies
	}

	void Start()
	{			
		mWeapons = new SortedList();
		
		Weapon sword = new Sword();
		mCurrentWeapon = sword;
		mWeapons.Add (sword.Range, sword);
				
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
