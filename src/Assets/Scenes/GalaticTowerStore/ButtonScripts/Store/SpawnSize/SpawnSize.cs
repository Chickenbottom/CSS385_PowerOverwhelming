using UnityEngine;
using System.Collections;

public class SpawnSize : ButtonBehaviour {

	public TowerStoreBehavior TowerStore;
	public GUIText SpawnSizeText;
	public static float BonusValue, BonusLevel = 1;
	public GUIText mTotalGold;

	public void NewValue(int newLevel){
		TowerStore.mCurBonusType = BonusType.SpawnSize;
		//BonusLevel = float.Parse(SpawnSizeText.text);
		//BonusValue = TowerStore.TempUpGrades[(int)TowerStore.mCurBonusSubject, (int)TowerStore.mCurBonusType];
		BonusLevel += newLevel;
		TowerStore.mCurQuantity = 1 + BonusLevel * 0.2f;
		SpawnSizeText.text = BonusLevel.ToString();
		
		TowerStore.SetUpgrade();
	}
	public int GetOriginal(){
		return (int)TowerStore.GetOringalValue(TowerStore.mCurBonusSubject, TowerStore.mCurBonusType);
		
	}
}
