using UnityEngine;
using System.Collections;

public class AbilityTowerBehavior: TowerBehavior {

	public float mAbilityCooldown = 30f;
	float mHealingAbility = 25f;
	float mSpawnBonus = 1f;

	public float mHealthBonus{get; set;}


	public override void Click()
	{
		Debug.Log("CLICKED" + type + " tower " + health );
		try{
			rightClicked.GetComponent<TowerBehavior>().health += mHealingAbility * mHealthBonus;
		}
  	   	catch(UnityException e)
		{}
	   
	}
	
	void Start()
	{
		type = TOWERTYPE.Ability;
		health = 100;
		SharedStart();
		InvokeRepeating("AbilityCoolDown", 1f,1f);
	}
	void Update()
	{
		if(Input.GetMouseButtonUp(1)){
			rightClicked = this.gameObject;
		}
	}
	void AbilityCoolDown(){
		mAbilityCooldown -= 1;
		if(mAbilityCooldown == 0)
			mAbilityCooldown = 0;
	}
}
