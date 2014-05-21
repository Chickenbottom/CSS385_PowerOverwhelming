using UnityEngine;
using System.Collections;
using System;

public class TowerStoreBehavior : MonoBehaviour {

	private Upgrades mCurUpgrades; 
	public float[,] DynamicUpgrades;
	public float[,] OriginalUpgrades;

	public BonusSubject mCurBonusSubject{get; set; }
	public BonusType mCurBonusType{get; set; }
	public float mCurQuantity{get;set;}
	public int mCurCost{ get; set;}
	public GameObject TheStore;

	int mSubjectMax = Enum.GetNames(typeof(BonusSubject)).Length;
	int mBonusMax = Enum.GetNames(typeof(BonusType)).Length;

	// Use this for initialization
	void Start () {
		DynamicUpgrades = new float[mSubjectMax, mBonusMax];
		OriginalUpgrades = new float[mSubjectMax, mBonusMax];
		mCurUpgrades =  GameObject.Find("Store").GetComponent<Upgrades>();
		mCurUpgrades.SetTowerArray(ref DynamicUpgrades);
		mCurUpgrades.SetTowerArray(ref OriginalUpgrades);
	}
	
	public void Purchase(){
		//GameState.Gold -= mCurCost;
		mCurUpgrades.SetUpgradeArray(DynamicUpgrades);
		mCurUpgrades.SetTowerArray(ref OriginalUpgrades);
		mCurUpgrades.WriteBonuses();
	}
	public float GetUpgrade(){
		return DynamicUpgrades[(int)mCurBonusSubject, (int)mCurBonusType] = mCurQuantity;
	}
	public void SetUpgrade(){
		DynamicUpgrades[(int)mCurBonusSubject, (int)mCurBonusType] = mCurQuantity;
		//mCurUpgrades.SetBonus(mCurBonusSubject, mCurBonusType, mCurQuantity);
	}
	public void CloseStore(){
		TheStore.SetActive(false);
	}
	public int GetOringalValue(BonusSubject sub, BonusType typ){
		return SelectBar.ConvertBonusToInt(OriginalUpgrades[(int)mCurBonusSubject, (int)mCurBonusType]);
	}

}
