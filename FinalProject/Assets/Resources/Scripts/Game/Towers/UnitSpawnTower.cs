using UnityEngine;
using System.Collections;

public class UnitSpawnTower : TowerBehavior {


	enum STATUS{
		ENABLED,
		DISABLED,
	};

	//SqaudManager mSquadMan;
	TowerBehavior towerManager;
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


		string myTower = " ";
		string Path = "";
		if(mTowerType == TOWERTYPE.MELEE){
			myTower = "Melee: ";
			Path = "MeleeGUIText";
		}
		if(mTowerType == TOWERTYPE.RANGED){
			myTower = "Ranged: ";
			Path = "RangedGUIText";
		}
		if(mTowerType == TOWERTYPE.MAGIC){
			myTower = "Magic: ";
			Path = "MagicGUIText";

		}

		GameObject heal = GameObject.Find(Path);
		GUIText gui = heal.GetComponent<GUIText>();
		gui.text = myTower + mTowerHealth.ToString();
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
		
		
		string myTower = " ";
		string Path = "DestinationGUIText";
		if(mTowerType == TOWERTYPE.MELEE){
			myTower = "Melee: ";
		}
		if(mTowerType == TOWERTYPE.RANGED){
			myTower = "Ranged: ";
		}
		if(mTowerType == TOWERTYPE.MAGIC){
			myTower = "Magic: ";			
		}
		
		GameObject heal = GameObject.Find(Path);
		GUIText gui = heal.GetComponent<GUIText>();
		gui.text = myTower + destination.ToString();
			//pass destination to squad 
	}
	public float gethealth(){
		return mTowerHealth;
	}

}
