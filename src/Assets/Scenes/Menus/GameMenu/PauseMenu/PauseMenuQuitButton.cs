using UnityEngine;
using System.Collections;

public class PauseMenuQuitButton : ButtonBehaviour {

	public GameObject mConfirmQuitFrame;
	public GameObject mPauseMenuFrame;

	void OnMouseDown(){
		ChangeScreen();
		mConfirmQuitFrame.SetActive(true);
		mPauseMenuFrame.SetActive(false);
	}
}
