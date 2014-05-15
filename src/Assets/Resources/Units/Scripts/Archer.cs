using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Archer : Unit
{
	protected new void Awake()
	{
		base.Awake();
		
		defaultHealth = 4;
		
		movementSpeed = 8f; // units per second
		chargeSpeed = 10f;   // speed used to engage enemies
	}
	
	void Start()
	{
		weapons = new SortedList();
		
		Weapon crossbow = new Crossbow();
		currentWeapon = crossbow;
		weapons.Add (crossbow.Range, crossbow);
		
		Weapon dagger = new Dagger();
		weapons.Add (dagger.Range, dagger);
		
		health = defaultHealth;
		previousHealth = health;
		sprites = null;
	}
}
