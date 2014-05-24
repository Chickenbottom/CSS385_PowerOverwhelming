using UnityEngine;
using System.Collections;

public class GTSButtonBehavior : ButtonBehaviour {

	public TowerStoreBehavior mStore;
	public GUIText mText;
	public BonusType mBonusType;

	protected int mBonusLevel;
	protected const int kBonusMax = 5;
	protected const int kBonusMin = 0;
	public GUIText mTotalGoldText;


	void Update(){
		mBonusLevel = mStore.GetUpgrade(mStore.mCurBonusSubject, mBonusType);	
		mText.text = mBonusLevel.ToString();
	}
	public void NewValue(int newLevel){
		mBonusLevel += newLevel;
		mStore.SetUpgrade(mStore.mCurBonusSubject, mBonusType, mBonusLevel);
	}
	public int GetOriginal(){
		return mStore.GetOringalValue(mStore.mCurBonusSubject, mBonusType);
	}
}
