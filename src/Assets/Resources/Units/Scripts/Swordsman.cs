using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swordsman : Unit
{
	protected new void Awake()
	{
		base.Awake();
		defaultHealth = 4;
		
		movementSpeed = 10f; // units per second
		chargeSpeed = 15f;   // speed used to engage enemies
		
		weapons = new SortedList();
		
		Weapon sword = new Sword();
		currentWeapon = sword;
		weapons.Add (sword.Range, sword);
	}

	void Start()
	{			
		health = defaultHealth;
		previousHealth = health;
		
		sprites = new List<Sprite>();
		sprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman", typeof(Sprite)) as Sprite);
		sprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman3", typeof(Sprite)) as Sprite);
		sprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman2", typeof(Sprite)) as Sprite);
		sprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman1", typeof(Sprite)) as Sprite);
		sprites.Add (Resources.Load("Textures/FriendlySwordsman/f_swordsman", typeof(Sprite)) as Sprite);
	}
}
