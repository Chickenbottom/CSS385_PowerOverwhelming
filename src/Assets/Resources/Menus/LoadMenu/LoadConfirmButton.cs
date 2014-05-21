using UnityEngine;
using System.Collections;

public class LoadConfirmButton : ButtonBehaviour {

	public GameObject mLoadMenuFrame;
	public GameObject mNarativeContinueMenuFrame;
	void OnMouseDown(){
		ChangeScreen();
		mLoadMenuFrame.SetActive(false);
		mNarativeContinueMenuFrame.SetActive(true);
	}
}
