using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	
	public enum ATTACHBAR{
		ATTACHED,
		DETACHED,
	};

    float hitPoints = 1;
    float maxHitPoints = 100;

	public GameObject mProgressBarPrefab;
	public GameObject mInfoObject;
	public GameObject mPositionObject;
	public TOWERTYPE mTowerType;
	public ATTACHBAR mAttached;
	public float offsetX;
	public float offsetY;
	public float mBarHeight = 5;
	public float mHealthBarWidthMultiplier = 20;
	public float mWidthMultiplier = 30;
	
	GameObject mHB = null;
	float mBarWidth = 20;

	
	void Start()
	{

		mHB = (GameObject) Instantiate (mProgressBarPrefab, transform.position, transform.rotation);
		if(mAttached == ATTACHBAR.ATTACHED)
			assignHitPoints();
		else
			assignXPCooldownPoints();
	}

	
	void Update()
	{
		Vector3 tempPosition;
		tempPosition = Camera.main.WorldToViewportPoint (mPositionObject.transform.position);
		tempPosition.x += offsetX;
		tempPosition.y += offsetY;
		tempPosition.z = 0.0f;
		mHB.transform.position = tempPosition;
		mHB.transform.localScale = Vector3.zero;

		if(mAttached == ATTACHBAR.ATTACHED)
			assignHitPoints();
		else
			assignXPCooldownPoints();

		float percent = hitPoints / maxHitPoints;
		if (percent < 0)
			percent = 0;
		if (percent > 100)
			percent = 100;
		if(mAttached == ATTACHBAR.ATTACHED)
			mBarWidth = percent * mHealthBarWidthMultiplier;
		else
			mBarWidth = percent * mWidthMultiplier;
			
		mHB.guiTexture.pixelInset = new Rect(10, 10, mBarWidth, mBarHeight);
	}
	void assignHitPoints(){
		switch(mTowerType){
		case TOWERTYPE.Ability:
			hitPoints = GameObject.Find(mInfoObject.name).GetComponent<AbilityTower>().health;
			maxHitPoints = GameObject.Find(mInfoObject.name).GetComponent<AbilityTower>().mMaxHealth;
			break;
		case TOWERTYPE.Unit:
			hitPoints = GameObject.Find(mInfoObject.name).GetComponent<UnitSpawnerTower>().health;
			maxHitPoints = GameObject.Find(mInfoObject.name).GetComponent<UnitSpawnerTower>().mMaxHealth;
			break;
		}
	}
	void assignXPCooldownPoints(){
		switch(mTowerType){
		case TOWERTYPE.Ability:
			hitPoints = GameObject.Find(mInfoObject.name).GetComponent<AbilityTower>().mAbilityCooldown;
			maxHitPoints = GameObject.Find(mInfoObject.name).GetComponent<AbilityTower>().mCooldownMax;
			break;
		case TOWERTYPE.Unit: 
			hitPoints = GameObject.Find(mInfoObject.name).GetComponent<UnitSpawnerTower>().mXP;
			maxHitPoints = GameObject.Find(mInfoObject.name).GetComponent<UnitSpawnerTower>().mXPMax;
			break;
		}
	}
}
