using UnityEngine;
using System.Collections;

public class TowerHeal : ButtonBehaviour {

	public TowerStoreBehavior TowerStore;
	public GUIText TowerHealthText;
	public static float BonusValue, BonusLevel = 1;
	public GUIText mTotalGold;

	public void NewValue(int newLevel){
		TowerStore.mCurBonusType = BonusType.Health;
		//BonusValue = float.Parse(TowerHealthText.text);
		//BonusValue = TowerStore.TempUpGrades[(int)TowerStore.mCurBonusSubject, (int)TowerStore.mCurBonusType];
		BonusLevel += newLevel;
		TowerStore.mCurQuantity = 1 + BonusLevel * 0.2f;
		TowerHealthText.text = BonusLevel.ToString();
		
		TowerStore.SetUpgrade();
	}
	public int GetOriginal(){
		return (int)TowerStore.GetOringalValue(TowerStore.mCurBonusSubject, TowerStore.mCurBonusType);
		
	}
}
