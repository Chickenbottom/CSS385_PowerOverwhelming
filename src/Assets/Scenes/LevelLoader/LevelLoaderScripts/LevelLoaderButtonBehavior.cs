using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Era{
	Prehistoric = 0,
	Medieval = 1,
	Japanese = 2,
	ModernAmerica = 3,
	Future = 4,
};
public enum EraLevel{
	_1 = 1,
	_2 = 2,
};
public class LevelLoaderButtonBehavior : ButtonBehaviour {


	public Sprite mLocked;
	public Sprite mLockedOver;
	public Era mEra;
	public GameObject mLevelSelectionButtons;
	public GameObject mEraButtons;
	public bool mMultipleLevels;

	bool mLevelLocked;

	// Use this for initialization
	void Start () {

		//GameState.CurrentEra = Era.Japanese;
		if(mEra <= GameState.CurrentEra)
			mLevelLocked = false;
		else
			mLevelLocked = true;

		if(mLevelLocked){
			mButton = mLocked;
			mButtonOver = mLockedOver;
		}
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButton;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if(mEra <= GameState.CurrentEra){
			if(mMultipleLevels){
				ChangeScreen();	
				DifficultyLoader.mCurrentEra = mEra;
				mLevelSelectionButtons.SetActive(true);
				mEraButtons.SetActive(false);
				GameObject.Find("LevelPicture").GetComponent<SpriteRenderer>().sprite = mButton;
				mEraButtons.SetActive(false);
			}
			else{
				Application.LoadLevel(Enum.GetName(typeof(Era),mEra) + Enum.GetName(typeof(EraLevel), EraLevel._1));
			}
		}
	}
	public Era GetEra(){
		return mEra;
	}

}
