using UnityEngine;
using System.Collections;

public class LoadButton : ButtonBehaviour {


	public GameObject mLoadFrameObject;
	public GameObject mMenuFrameObject;

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		SaveLoad s = GameObject.Find("SaveLoad").GetComponent<SaveLoad>();
		s.Clear(SaveLoad.SAVEFILE.Level);
		s.Load(SaveLoad.SAVEFILE.Level);
		//ChangeScreen();
		//mLoadMenuFrame.SetActive(false);
		//mNarativeContinueMenuFrame.SetActive(true);
		Application.LoadLevel("LevelLoader");

		//mLoadFrameObject.SetActive(true);
		//mMenuFrameObject.SetActive(false);
		//ChangeScreen();
	}
}
