﻿using UnityEngine;
using System.Collections;

public class Peasant : Unit 
{
	protected new void Awake()
	{
		base.Awake();
	
		sightRange = 10f;
		defaultHealth = 5;
		
		movementSpeed = 8f; // units per second
		chargeSpeed = 10f;   // speed used to engage enemies
	}
	
	void Start()
	{	
		weapons = new SortedList();
			
		Weapon pitchfork = new Pitchfork();
		weapons.Add (pitchfork.Range, pitchfork);
		currentWeapon = pitchfork;
		
		health = defaultHealth;
		previousHealth = health;
		sprites = null;
	}
}