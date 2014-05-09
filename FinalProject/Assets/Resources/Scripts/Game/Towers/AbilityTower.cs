using UnityEngine;
using System.Collections;

public class AbilityTowerBehavior: TowerBehavior {

	const float kHealRate = 50;
	float mHealBonus = 1;
	public TOWERTYPE mTowerType;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		setSelected(this);
	}
	public void setHealBonus(float bonus = 1){
		mHealBonus = bonus;
	}
	public float getHealRate(){
		return kHealRate * mHealBonus;
	}
	public override TOWERTYPE getMyTowerType(){
		return mTowerType;
	}

    public override void Click()
    {
        throw new System.NotImplementedException();
    }

}
