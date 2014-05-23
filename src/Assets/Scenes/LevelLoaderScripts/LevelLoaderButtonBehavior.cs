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

public class LevelLoaderButtonBehavior : MonoBehaviour {


	public Sprite mButtonDown;
	public Sprite mButton;
	public Sprite mLocked;
	public Sprite mLockedDown;
	public Sprite mButtonSelected;
	public Era mEra;
	public GameObject mLevelSelectionButtons;

	bool mLevelLocked;

	// Use this for initialization
	void Start () {

        

		//mLevelSelectionButtons.SetActive(false);
        GameState.CurrentEra = Era.Medieval;
		if(mEra <= GameState.CurrentEra)
			mLevelLocked = false;
		else
			mLevelLocked = true;

		if(mLevelLocked){
			mButton = mLocked;
			mButtonDown = mLockedDown;
		}
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButton;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseOver(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButtonDown;
	}
	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButton;
	}
	void OnMouseDown(){
		if(mEra <= GameState.CurrentEra){
			mLevelSelectionButtons.SetActive(true);
			gameObject.GetComponent<SpriteRenderer> ().sprite = mButtonSelected;
		}
	}
	public Era GetEra(){
		return mEra;
	}
}
