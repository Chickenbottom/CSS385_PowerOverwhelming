using UnityEngine;
using System.Collections;
using System;

public class TowerStoreBehavior : MonoBehaviour {

	private Upgrades mCurUpgrades; 
	public int[,] DynamicUpgrades;
	public int[,] OriginalUpgrades;

	public BonusSubject mCurBonusSubject{get; set; }
//	public BonusType mCurBonusType{get; set; }

//	public float mCurQuantity{get;set;}
	public int mCurCost{ get; set;}

	public GameObject TheStore;

	int mSubjectMax = Enum.GetNames(typeof(BonusSubject)).Length;
	int mBonusMax = Enum.GetNames(typeof(BonusType)).Length;

	// Use this for initialization
	void Start () {
		mCurBonusSubject = BonusSubject.Special;
		DynamicUpgrades = new int[mSubjectMax, mBonusMax];
		OriginalUpgrades = new int[mSubjectMax, mBonusMax];
		mCurUpgrades =  GameObject.Find("Store").GetComponent<Upgrades>();
		mCurUpgrades.SetTowerArray(ref DynamicUpgrades);
		mCurUpgrades.SetTowerArray(ref OriginalUpgrades);
	}
	
	public void Purchase(){
		//GameState.Gold -= mCurCost;
		mCurUpgrades.SetUpgradeArray(DynamicUpgrades);
		//mCurUpgrades.SetTowerArray(ref OriginalUpgrades);
		mCurUpgrades.WriteBonuses();
	}
	public float GetUpgrade(BonusSubject curSub, BonusType curType){
		return DynamicUpgrades[(int)curSub, (int)curType];
	}
	public void SetUpgrade(BonusSubject curSub, BonusType curType, int curValue ){
		DynamicUpgrades[(int)curSub, (int)curType] = curValue;
		//mCurUpgrades.SetBonus(mCurBonusSubject, mCurBonusType, mCurQuantity);
	}
	public void CloseStore(){
		TheStore.SetActive(false);
	}
	public int GetOringalValue(BonusSubject sub, BonusType typ){
		return OriginalUpgrades[(int)sub, (int)typ];
	}

}
