using UnityEngine;
using System.Collections;

public enum Buttons{
	AboutButton,
	MenuButton,
	NewGameButton,
	LoadButton,
	Continue,
	Back,
};

public class AboutMenuButton : ButtonBehaviour {
	
	public GameObject mAboutObject;
	public GameObject mMainMenuObject;


//	public Buttons myButton;

	// Use this for initialization
	void Start () {

	}

	void OnMouseDown(){
		mMainMenuObject.SetActive(true);
		mAboutObject.SetActive(false);
		ChangeScreen();
		
	}
}
