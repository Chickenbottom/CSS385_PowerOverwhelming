using UnityEngine;
using System.Collections;

public class UnitSpawnTower : TowerBehavior {


	enum STATUS{
		ENABLED,
		DISABLED,
	};

	SqaudManager mSquadMan;
	const int kSquadSpawnTime;
	TOWERTYPE mTowerType;
	int mSquadLastSpawn;
	float mSpawnBonus = 1;
	Unit mUnityType;
	float towerHealth;
	STATUS mStatus;
	// Use this for initialization
	void Start () {
		mStatus = STATUS.ENABLED;
	}
	
	// Update is called once per frame
	void Update () {

		if(towerHealth <= 0){
			if(mStatus == STATUS.DISABLED)
				mStatus = STATUS.ENABLED;
			else
				mStatus = STATUS.DISABLED;
		}


	}
	void spawnSquad(){
		if(Time.realtimeSinceStartup - mSquadLastSpawn > kSquadSpawnTime * mSpawnBonus){
			//mSquadMan.create();
		}
	}
	void OnMouseDown(){
		if(mStatus == STATUS.ENABLED)
			setSelected(this);
	}
	void Click(){
		//mSquadMan.setSqaudDesination(Vector3 Click);
	}
	public void healTower(float h){
		towerHealth += h;
	}
	public void hitTower(float h){
		towerHealth -= h;
	}
	public TOWERTYPE getMyTowerType(){
		return mTowerType;
	}

}
