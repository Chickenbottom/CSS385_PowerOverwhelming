using UnityEngine;
using System.Collections;

public class King : Unit {
	private Progressbar HealthBar;
	
	protected new void Awake()
	{
		base.Awake();
		
		mSightRange = 5f;
		mDefaultHealth = 100;
		
		mMovementSpeed = 1f; // units per second
		mChargeSpeed = 1f;   // speed used to engage enemies
	}
	
	public void Start()
	{
		mWeapons = new SortedList();
		Weapon sword = new Sword();
		mCurrentWeapon = sword;
		mWeapons.Add (sword.Range, sword);
		
		mHealth = mDefaultHealth;
		mPreviousHealth = mHealth;
		
		GameObject o = GameObject.Find ("KingsHealthBar") as GameObject;
		if (o == null)
			Debug.Log ("King's Health Bar could not be loaded.");
			
		this.HealthBar = o.GetComponent<Progressbar>();
		this.HealthBar.MaxValue = mDefaultHealth;
		this.HealthBar.CurrentValue = mHealth;
	}
	
	public override void Damage (int damage)
	{
		base.Damage (damage);
		if (mHealth <= 0)
			GameState.TriggerLoss();
			
		Debug.Log ("King's Health: " + mHealth);
		this.HealthBar.UpdateValue(mHealth);
	}
}
