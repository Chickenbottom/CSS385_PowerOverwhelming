using UnityEngine;
using System.Collections;

public class NewGameButton : ButtonBehaviour {
	public GameObject mSaveMenuObject;
	public GameObject mNewGameObject;

	void OnMouseDown(){
		mNewGameObject.SetActive(true);
		mSaveMenuObject.SetActive(false);
	}

}

