using UnityEngine;
using System.Collections;

public class NewGameButton : ButtonBehaviour {
	public GameObject mSaveMenuObject;
	public GameObject mMenuObject;

	void OnMouseDown(){
		mSaveMenuObject.SetActive(true);
		mMenuObject.SetActive(false);
		ChangeScreen();
		
	}

}

