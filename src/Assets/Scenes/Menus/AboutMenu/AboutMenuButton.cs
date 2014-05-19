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
	
	public GUIText mText;
	public GameObject mAboutObject;
	public GameObject mMainMenuObject;


//	public Buttons myButton;

	// Use this for initialization
	void Start () {
		mText.text = "Developers\t\t\t\t\n";
		mText.text += "James Murphee \nRodelle something\nChad Hickenbottom \n";
		mText.text += "Artist\t\t\t\t\t\n";
		mText.text += "Angela Something\n";
		mText.text += "Music\t\t\t\t\t\n";
		mText.text += "Jd Awald"; 
	}

	void OnMouseDown(){
		mMainMenuObject.SetActive(true);
		mAboutObject.SetActive(false);
	}
}
