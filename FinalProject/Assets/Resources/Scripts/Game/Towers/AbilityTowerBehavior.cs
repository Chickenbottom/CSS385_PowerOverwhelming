using UnityEngine;
using System.Collections;

public class AbilityTowerBehavior: TowerBehavior {

	const float kHealRate = 50 ;
	float mHealBonus = 1;
	public TOWERTYPE mTowerType;
	float mTowerHealth = 100;
	TowerBehavior towerManager;
	STATUS mStatus;

	// Use this for initialization
	void Start () {
		towerManager = GameObject.Find ("TowerManager").GetComponent<TowerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {

		if(mTowerHealth <= 0){
			if(mStatus == STATUS.DISABLED)
				mStatus = STATUS.ENABLED;
			else
				mStatus = STATUS.DISABLED;

		}
	}
	public override void Click(Vector3 destination){
		//heal tower
	}
	void OnMouseDown(){
		if(mStatus == STATUS.ENABLED){
			//towerManager.setSelected(null, this.GetComponent<AbilityTowerBehavior>());
			mouseManager.Select(gameObject);
		}
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
