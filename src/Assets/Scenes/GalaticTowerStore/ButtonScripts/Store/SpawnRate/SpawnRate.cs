using UnityEngine;
using System.Collections;

public class SpawnRate : ButtonBehaviour {

	public TowerStoreBehavior TowerStore;
	public GUIText SpawnRateText;
	public static float BonusValue, BonusLevel = 1;
	
	public void NewValue(int newLevel ){
		TowerStore.mCurBonusType = BonusType.SpawnRate;
		//BonusValue = float.Parse(SpawnRateText.text);
		//BonusValue = TowerStore.TempUpGrades[(int)TowerStore.mCurBonusSubject, (int)TowerStore.mCurBonusType];
		BonusLevel += newLevel;
		TowerStore.mCurQuantity = 1 + BonusLevel * 0.2f;
		SpawnRateText.text = BonusLevel.ToString();
		
		TowerStore.SetUpgrade();
	}
	public int GetOriginal(){
		return (int)TowerStore.GetOringalValue(TowerStore.mCurBonusSubject, TowerStore.mCurBonusType);
		
	}
}
