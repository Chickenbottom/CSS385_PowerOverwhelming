using UnityEngine;
using System.Collections;

public class NewGameButton : ButtonBehaviour {
	//public GameObject mSaveMenuObject; //uncomment after save load is working

	//remove after save / load is working
	public GameObject mNarativeContinueFrame;
	public GameObject mMenuObject;

	void OnMouseDown(){

		//after save / load game is up and running
		//mSaveMenuObject.SetActive(true);


		//remove after save load is working
		mNarativeContinueFrame.SetActive(true);
		mMenuObject.SetActive(false);
		ChangeScreen();
		
	}

}

