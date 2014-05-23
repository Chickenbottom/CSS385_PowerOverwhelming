using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectBar : MonoBehaviour {

	//public TowerStoreBehavior mStore;
	//public BonusSubject mBonusSubject;
	//public GUIText CoolDownText, SpawnRateText, SpawnSizeText, TowerHealthText;

	//int cooldown, spawnrate, spawnsize, towerhealth;


	public List<GameObject> mAllTabs;
	public List<GameObject> mAllTabButtons;
	GameObject mCurrentTab;
	GameObject mCurrentTabButtons;


	void Start(){
		for(int i = 0; i < mAllTabs.Count; i++){
			GameObject tempTab = (GameObject)mAllTabs[i];
			GameObject tempTabButtons = (GameObject)mAllTabButtons[i];
			if(tempTab.name.Equals("TabsSpecial")){
				mCurrentTab = tempTab;
				mCurrentTab.renderer.sortingOrder = 10;
			}
			else
				tempTab.renderer.sortingOrder = 1;
			if(tempTabButtons.name.Equals("SpecialTabButtons")){
				mCurrentTabButtons = tempTabButtons;
				mCurrentTabButtons.SetActive(true);
			}
			else
				tempTabButtons.SetActive(false);
		}
		//start with the special tab
		//find tab and tab buttons
	//	GameObject tempT = GameObject.Find("TabsSpecial");
	//	GameObject tempTB =  GameObject.Find("SpecialTabButtons");

		//top sorting order
	//	tempT.GetComponent<Renderer>().sortingOrder = 10;
		//turn buttons on
	//	tempTB.SetActive(true);

		//assign to current values;
	//	mCurrentTab = tempT;
	//	mCurrentTabButtons = tempTB;
	}


	public void SetTab(GameObject CurTab, GameObject TabButtons){

		mCurrentTab.renderer.sortingOrder = 1;
		mCurrentTabButtons.SetActive(false);

		mCurrentTab	= CurTab;
		mCurrentTabButtons = TabButtons;

		mCurrentTab.renderer.sortingOrder = 10;
		mCurrentTabButtons.SetActive(true);



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
