using UnityEngine;
using System.Collections;

public class CoolDown : ButtonBehaviour {

	public TowerStoreBehavior TowerStore;
	public GUIText CoolDownText;
	public static int mBonusLevel = 1;
	private const float kBonusValue = 0.2f;

	public void NewValue(int newLevel){

		TowerStore.mCurBonusType = BonusType.CoolDown;
		mBonusLevel += newLevel;
		TowerStore.mCurQuantity = 1 + mBonusLevel * kBonusValue;
		CoolDownText.text = mBonusLevel.ToString();
		TowerStore.SetUpgrade();
	}
	public int GetOriginal(){
		return (int)TowerStore.GetOringalValue(TowerStore.mCurBonusSubject, TowerStore.mCurBonusType);
		
	}
}
