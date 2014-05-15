using UnityEngine;
using System.Collections;

public class King : Unit {
	private ProgressBar HealthBar;
	
	protected new void Awake()
	{
		base.Awake();
		
		sightRange = 5f;
		defaultHealth = 100;
		
		movementSpeed = 1f; // units per second
		chargeSpeed = 1f;   // speed used to engage enemies
	}
	
	public void Start()
	{
		weapons = new SortedList();
		Weapon sword = new Sword();
		currentWeapon = sword;
		weapons.Add (sword.Range, sword);
		
		health = defaultHealth;
		previousHealth = health;
		
		GameObject o = GameObject.Find ("KingsHealthBar") as GameObject;
		if (o == null)
			Debug.Log ("King's Health Bar could not be loaded.");
			
		this.HealthBar = o.GetComponent<ProgressBar>();
		this.HealthBar.maxValue = defaultHealth;
		this.HealthBar.currentValue = health;
	}
	
	public override void Damage (int damage)
	{
		base.Damage (damage);
		if (health <= 0)
			GameState.TriggerLoss();
			
		Debug.Log ("King's Health: " + health);
		this.HealthBar.UpdateValue(health);
	}
}
