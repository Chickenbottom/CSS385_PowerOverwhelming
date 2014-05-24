using UnityEngine;
using System.Collections;

public class CoolDown : ButtonBehaviour {

	public TowerStoreBehavior mStore;
	public GUIText CoolDownText;


	protected int mBonusLevel;
	protected const int kBonusMax = 5;
	protected const int kBonusMin = 1;

	public GUIText mTotalGoldText;


	void Update(){
		int cooldown = mStore.DynamicUpgrades[(int)mStore.mCurBonusSubject, (int)BonusType.CoolDown];	
		CoolDownText.text = cooldown.ToString();
	}
	public void NewValue(int newLevel){
		mBonusLevel += newLevel;
		mStore.SetUpgrade(mStore.mCurBonusSubject, BonusType.CoolDown, mBonusLevel);
	}
	public int GetOriginal(){
		return (int)mStore.GetOringalValue(mStore.mCurBonusSubject, BonusType.CoolDown);
	}
}
