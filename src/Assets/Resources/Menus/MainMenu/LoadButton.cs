using UnityEngine;
using System.Collections;

public class LoadButton : ButtonBehaviour {


	public GameObject mLoadFrameObject;
	public GameObject mMenuFrameObject;

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		mLoadFrameObject.SetActive(true);
		mMenuFrameObject.SetActive(false);
		ChangeScreen();
	}
}
