using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectBar : MonoBehaviour {

	//public TowerStoreBehavior mStore;
	//public BonusSubject mBonusSubject;
	//public GUIText CoolDownText, SpawnRateText, SpawnSizeText, TowerHealthText;

	//int cooldown, spawnrate, spawnsize, towerhealth;

	public List<GameObject> mAllTabs;
	GameObject mCurrentTab;

	public void SetTab(GameObject CurTab){
		for(int i = 0; i < mAllTabs.Count; i++){
			GameObject temp = (GameObject)mAllTabs[i];
			temp.renderer.sortingOrder = 1;
		}
		mCurrentTab = CurTab;
		mCurrentTab.renderer.sortingOrder = 10;
				//mCurrentTab.layer = 11;
		/*
		mStore.mCurBonusSubject = mBonusSubject;

		cooldown = ConvertBonusToInt(mStore.DynamicUpgrades[(int)mBonusSubject, (int)BonusType.CoolDown]);	
		CoolDownText.text =  cooldown.ToString();

		spawnrate = ConvertBonusToInt(mStore.DynamicUpgrades[(int)mBonusSubject, (int)BonusType.SpawnRate]);
		SpawnRateText.text =  spawnrate.ToString();

		spawnsize =  ConvertBonusToInt(mStore.DynamicUpgrades[(int)mBonusSubject, (int)BonusType.SpawnSize]);
		SpawnSizeText.text =  spawnsize.ToString();

		towerhealth = ConvertBonusToInt(mStore.DynamicUpgrades[(int)mBonusSubject, (int)BonusType.Health]);
		TowerHealthText.text =  towerhealth.ToString();
		*/
	}
	public static int ConvertBonusToInt(float percent){
		percent -= 1;
		if(percent <= 0)
			return 1;
		percent /= 0.2f;
		return (int)percent;
	}


}
