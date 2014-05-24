using UnityEngine;
using System.Collections;

public class SpawnRate : ButtonBehaviour {

	public TowerStoreBehavior mStore;
	public GUIText SpawnRateText;
	public static float BonusValue, BonusLevel = 1;
	public GUIText mTotalGoldText;
	protected const int kBonusMax = 5;
	protected const int kBonusMin = 1;

//	void Update(){
//		int SpawnR = mStore.ConvertBonusToInt(mStore.DynamicUpgrades[(int)mStore.mCurBonusSubject, (int)BonusType.CoolDown]);	
//		SpawnRateText.text =  SpawnR.ToString();
//	}

	public void NewValue(int newLevel ){
		//mStore.mCurBonusType = BonusType.SpawnRate;
//		BonusLevel += newLevel;
		//SpawnRateText.text = BonusLevel.ToString();

//		float value = 1 + BonusLevel * 0.2f;
//		mStore.SetUpgrade(mStore.mCurBonusSubject, BonusType.SpawnRate, value);
	}
//	public int GetOriginal(){
//		return (int)mStore.GetOringalValue(mStore.mCurBonusSubject, BonusType.SpawnRate);		
//	}
}
