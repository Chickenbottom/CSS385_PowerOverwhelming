using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectBar : MonoBehaviour {

	public List<GameObject> mAllTabs;
	public List<GameObject> mAllTabButtons;
	GameObject mCurrentTab;
	GameObject mCurrentTabButtons;
	public TowerStoreBehavior mStore;

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

	}


	public void SetTab(GameObject CurTab, GameObject TabButtons, BonusSubject curSub){

		mCurrentTab.renderer.sortingOrder = 1;
		mCurrentTabButtons.SetActive(false);

		mCurrentTab	= CurTab;
		mCurrentTabButtons = TabButtons;

		mCurrentTab.renderer.sortingOrder = 10;
		mCurrentTabButtons.SetActive(true);
	
		mStore.mCurBonusSubject = curSub;
	}



}
