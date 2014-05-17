using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mage : Unit
{
	protected new void Awake()
	{
		base.Awake();
		
		defaultHealth = 4;
		
		movementSpeed = 8f; // units per second
		chargeSpeed = 10f;   // speed used to engage enemies
		
		weapons = new SortedList();
		
		Weapon dagger = new Dagger();
		weapons.Add (dagger.Range, dagger);
		
		Weapon icewand = new IceWand();
		((IceWand) icewand).src = this;
		weapons.Add (icewand.Range, icewand);
		currentWeapon = icewand;
	}
	
	void Start()
	{		
		health = defaultHealth;
		previousHealth = health;
		sprites = null;
	}
}
