using UnityEngine;
using System.Collections;


public class SelectBar : MonoBehaviour {

	public TowerStoreBehavior mStore;
	public BonusSubject mBonusSubject;
	public GUIText CoolDownText, SpawnRateText, SpawnSizeText, TowerHealthText;

	int cooldown, spawnrate, spawnsize, towerhealth;

	void OnMouseDown(){
		mStore.mCurBonusSubject = mBonusSubject;

		cooldown = ConvertBonusToInt(mStore.DynamicUpgrades[(int)mBonusSubject, (int)BonusType.CoolDown]);	
		CoolDownText.text =  cooldown.ToString();

		spawnrate = ConvertBonusToInt(mStore.DynamicUpgrades[(int)mBonusSubject, (int)BonusType.SpawnRate]);
		SpawnRateText.text =  spawnrate.ToString();

		spawnsize =  ConvertBonusToInt(mStore.DynamicUpgrades[(int)mBonusSubject, (int)BonusType.SpawnSize]);
		SpawnSizeText.text =  spawnsize.ToString();

		towerhealth = ConvertBonusToInt(mStore.DynamicUpgrades[(int)mBonusSubject, (int)BonusType.Health]);
		TowerHealthText.text =  towerhealth.ToString();
	}
	public static int ConvertBonusToInt(float percent){
		percent -= 1;
		if(percent <= 0)
			return 1;
		percent /= 0.2f;
		return (int)percent;
	}


}
