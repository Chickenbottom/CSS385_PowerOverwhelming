using UnityEngine;
using System.Collections;

public class SpawnSize : ButtonBehaviour {

	public TowerStoreBehavior mStore;
	public GUIText SpawnSizeText;
	//public static float BonusValue, BonusLevel = 1;
	public GUIText mTotalGoldText;

	protected int mBonusLevel;
	protected const int kBonusMax = 5;
	protected const int kBonusMin = 1;


	void Update(){
		mBonusLevel = mStore.DynamicUpgrades[(int)mStore.mCurBonusSubject, (int)BonusType.SpawnSize];	
		SpawnSizeText.text =  mBonusLevel.ToString();
	}

	public void NewValue(int newLevel){
		mBonusLevel += newLevel;
		mStore.SetUpgrade(mStore.mCurBonusSubject, BonusType.SpawnSize, mBonusLevel);
	}
	public int GetOriginal(){
		return (int)mStore.GetOringalValue(mStore.mCurBonusSubject, BonusType.SpawnSize);
	}

}
