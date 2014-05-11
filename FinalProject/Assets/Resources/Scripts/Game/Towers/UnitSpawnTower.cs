using UnityEngine;
using System.Collections;

public class UnitSpawnTower : TowerBehavior {


	enum STATUS{
		ENABLED,
		DISABLED,
	};

	//SqaudManager mSquadMan;
	TowerBehavior towerManager ;
	int kSquadSpawnTime;
	public TOWERTYPE mTowerType;
	int mSquadLastSpawn;
	float mSpawnBonus = 1;
	//Unit mUnityType;
	float mTowerHealth = 100;
	STATUS mStatus;
	// Use this for initialization
	void Start () {
		mStatus = STATUS.ENABLED;
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
	void spawnSquad(){
		if(Time.realtimeSinceStartup - mSquadLastSpawn > kSquadSpawnTime * mSpawnBonus){
			//mSquadMan.create();
		}
	}
	void OnMouseDown(){
		if(mStatus == STATUS.ENABLED)
			towerManager.setSelected(this.GetComponent<UnitSpawnTower>());

	}
	void Click(){
		//mSquadMan.setSqaudDesination(Vector3 Click);
	}
	public void healTower(float h){
		mTowerHealth += h;
	}
	public void hitTower(float h){
		mTowerHealth -= h;
	}
	public TOWERTYPE getMyTowerType(){
		return mTowerType;
	}
	public void setDestination(Vector3 destination){		
			//pass destination to squad 
	}
	public float gethealth(){
		return mTowerHealth;
	}

}
