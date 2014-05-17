using UnityEngine;
using System.Collections;

public class Peasant : Unit 
{
	protected new void Awake()
	{
		base.Awake();
	
		SightRange = 10f;
		defaultHealth = 5;
		
		movementSpeed = 8f; // units per second
		chargeSpeed = 10f;   // speed used to engage enemies
		
		weapons = new SortedList();
		Weapon pitchfork = new Pitchfork();
		weapons.Add (pitchfork.Range, pitchfork);
		currentWeapon = pitchfork;
	}
	
	void Start()
	{		
		health = defaultHealth;
		previousHealth = health;
		sprites = null;
	}
}
