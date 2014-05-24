using UnityEngine;
using System.Collections;

public class TowerHeal : ButtonBehaviour {

	public TowerStoreBehavior mStore;
	public GUIText TowerHealthText;
	public GUIText mTotalGoldText;

	protected int mBonusLevel = 1;
	protected const int kBonusMax = 5;
	protected const int kBonusMin = 1;


	void Update(){
		int Health = mStore.DynamicUpgrades[(int)mStore.mCurBonusSubject, (int)BonusType.Health];	
		TowerHealthText.text =  Health.ToString();
	}
	public void NewValue(int newLevel){

		mBonusLevel += newLevel;
		mStore.SetUpgrade( mStore.mCurBonusSubject, BonusType.Health, mBonusLevel);
	}
	public int GetOriginal(){
		return (int)mStore.GetOringalValue(mStore.mCurBonusSubject, BonusType.Health);	
	}
}
