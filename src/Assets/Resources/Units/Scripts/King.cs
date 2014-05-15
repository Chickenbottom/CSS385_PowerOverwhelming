using UnityEngine;
using System.Collections;

public class King : Unit {
	
	protected new void Awake()
	{
		base.Awake();
		
		sightRange = 5f;
		defaultHealth = 100;
		
		movementSpeed = 1f; // units per second
		chargeSpeed = 1f;   // speed used to engage enemies
		
		GameState.KingsHealth = defaultHealth;
	}
	
	public void Start()
	{
		weapons = new SortedList();
		Weapon sword = new Sword();
		currentWeapon = sword;
		weapons.Add (sword.Range, sword);
		
		health = defaultHealth;
		previousHealth = health;
	}
	
	public override void Damage (int damage)
	{
		base.Damage (damage);
		GameState.KingsHealth = health;
	}
}
