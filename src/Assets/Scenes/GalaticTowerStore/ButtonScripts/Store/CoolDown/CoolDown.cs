using UnityEngine;
using System.Collections;

public class CoolDown : ButtonBehaviour {

	public TowerStoreBehavior TowerStore;
	public GUIText CoolDownText;
	public static int BonusLevel = 1;
	private float BonusValue = 0.2f;

	public void NewValue(int newLevel){

		TowerStore.mCurBonusType = BonusType.CoolDown;
		BonusLevel += newLevel;
		TowerStore.mCurQuantity = 1 + BonusLevel * BonusValue;
		CoolDownText.text = BonusLevel.ToString();
		TowerStore.SetUpgrade();
	}
	public int GetOriginal(){
		return (int)TowerStore.GetOringalValue(TowerStore.mCurBonusSubject, TowerStore.mCurBonusType);
		
	}
}
