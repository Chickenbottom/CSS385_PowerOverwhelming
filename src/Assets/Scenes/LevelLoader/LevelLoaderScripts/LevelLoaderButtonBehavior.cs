using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum Era{
	Prehistoric = 0,
	Medieval = 1,
	Japanese = 2,
	ModernAmerica = 3,
	Future = 4,
};

public class LevelLoaderButtonBehavior : ButtonBehaviour {


	public Sprite mLocked;
	public Sprite mLockedOver;
//	public Sprite mButtonSelected;
	public Era mEra;
	public GameObject mLevelSelectionButtons;
//	public GameObject mEraButtons;

	bool mLevelLocked;

	// Use this for initialization
	void Start () {

        

		//mLevelSelectionButtons.SetActive(false);
        GameState.CurrentEra = (Era)int.Parse(GameObject.Find("SaveLoad").GetComponent<SaveLoad>().GetInfo(SaveLoad.SAVEFILE.Level)[0]);
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
	void OnMouseOver(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButtonOver;
	}
	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButton;
	}
	void OnMouseDown(){
		if(mEra <= GameState.CurrentEra){
			ChangeScreen();	
			DifficultyLoader.mCurrentEra = mEra;
			mLevelSelectionButtons.SetActive(true);
			GameObject.Find("LevelPicture").GetComponent<SpriteRenderer>().sprite = mButton;
			//mEraButtons.SetActive(false);
			GameObject.Find("EraButtons").SetActive(false);
		}
	}
	public Era GetEra(){
		return mEra;
	}

}
