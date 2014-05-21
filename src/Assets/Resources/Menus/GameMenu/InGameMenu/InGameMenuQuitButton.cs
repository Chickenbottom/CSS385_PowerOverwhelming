using UnityEngine;
using System.Collections;

public class InGameMenuQuitButton : ButtonBehaviour {

	public GameObject mInGameMenuFrame;

	void OnMouseDown(){
		ChangeScreen();
		mInGameMenuFrame.SetActive(false);
	}
}
