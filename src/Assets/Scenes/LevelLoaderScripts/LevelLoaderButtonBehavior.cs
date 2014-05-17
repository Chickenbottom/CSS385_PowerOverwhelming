using UnityEngine;
using System.Collections;



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
	public Era mEra;

	bool mLevelLocked;

	// Use this for initialization
	void Start () {

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
		if(mEra < GameState.CurrentEra){
			switch(mEra){
			case Era.Prehistoric:
				Application.LoadLevel("TowerStore");
				break;
			case Era.Medieval:
				Application.LoadLevel("Level2");
				break;
			case Era.Japanese:
				Application.LoadLevel("TowerStore");
				break;
			case Era.ModernAmerica:
				Application.LoadLevel("TowerStore");
				break;
			case Era.Future:
				Application.LoadLevel("TowerStore");
				break;
			}
		}
	}
}
