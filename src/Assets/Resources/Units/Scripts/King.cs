using UnityEngine;
using System.Collections;

public class King : Unit {
	
	protected new void Awake()
	{
		base.Awake();
		
		SightRange = 0f;
		defaultHealth = 100;
		
		movementSpeed = 0f; // units per second
		chargeSpeed = 0f;   // speed used to engage enemies
		
		GameState.KingsHealth = defaultHealth;
		
		weapons = new SortedList();
		Weapon sword = new Sword();
		currentWeapon = sword;
		weapons.Add (sword.Range, sword);
	}
	
	public void Start()
	{
		health = defaultHealth;
		previousHealth = health;
	}
	
	public override void Damage (int damage)
	{
		base.Damage (damage);
		GameState.KingsHealth = health;
	}
}
