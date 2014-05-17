using UnityEngine;
using System.Collections;



public enum CurrentLevel{
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
	public CurrentLevel myLevel;

	bool mLevelLocked;

	// Use this for initialization
	void Start () {

		GameState.gameLevel = CurrentLevel.Medieval;
		if(myLevel <= GameState.gameLevel)
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
		if(myLevel < GameState.gameLevel){
			switch(myLevel){
			case CurrentLevel.Prehistoric:
				Application.LoadLevel("TowerStore");
				break;
			case CurrentLevel.Medieval:
				Application.LoadLevel("Level2");
				break;
			case CurrentLevel.Japanese:
				Application.LoadLevel("TowerStore");
				break;
			case CurrentLevel.ModernAmerica:
				Application.LoadLevel("TowerStore");
				break;
			case CurrentLevel.Future:
				Application.LoadLevel("TowerStore");
				break;
			}
		}
	}
}
