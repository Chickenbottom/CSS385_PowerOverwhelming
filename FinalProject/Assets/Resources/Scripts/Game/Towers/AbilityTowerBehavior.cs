using UnityEngine;
using System.Collections;

public class AbilityTowerBehavior: TowerBehavior {

	const float kHealRate = 50 ;
	float mHealBonus = 1;
	public TOWERTYPE mTowerType;
	float mTowerHealth = 100;
	TowerBehavior towerManager;
	// Use this for initialization
	void Start () {
		towerManager = GameObject.Find ("TowerManager").GetComponent<TowerBehavior>();
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject heal = GameObject.Find("HealGUIText");
		GUIText gui = heal.GetComponent<GUIText>();
		gui.text = "Heal: " +  mTowerHealth.ToString();
	}
	void OnMouseDown(){
		towerManager.setSelected(null, this.GetComponent<AbilityTowerBehavior>());
	}
	public void setHealBonus(float bonus = 1){
		mHealBonus = bonus;
	}
	public float getHealRate(){
		return kHealRate * mHealBonus;
	}
	public TOWERTYPE getMyTowerType(){
		return mTowerType;
	}
	public void hitTower(float damage){
		mTowerHealth -= damage;
	}
	public void healTower(float heal){
		mTowerHealth += heal;
	}
}
