using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Menu : MonoBehaviour 
{
	// TODO replace these with dictionary mapped with Button enum
	//Rect mNewGameButton, mLoadButton, mAboutButton;
	
	Dictionary<Button, Rect> mButtons;
	
	protected enum Button {
		kNewGame,
		kLoadGame,
		kAbout
	}
	
	void OnGUI () 
	{
		//GUI.color = Color.clear;
		foreach(Button button in mButtons.Keys) {
			if (GUI.Button(mButtons[button], ""))
				HandleClick(button);
		}
	}
	
	private void HandleClick(Button button)
	{
		switch(button) 
		{
		case(Button.kNewGame):
			Application.LoadLevel("LevelLoader");
			break;
		case(Button.kLoadGame):
			break;
		case(Button.kAbout):
			break;
		}
	}
	
	void Awake()
	{
		mButtons = new Dictionary<Button, Rect>();
		mButtons.Add (Button.kNewGame, new Rect(313, 310, 400, 95));
		mButtons.Add (Button.kLoadGame, new Rect(313, 420, 400, 95));
		mButtons.Add (Button.kAbout, new Rect(313, 530, 400, 95));
		//mNewGameButton = new Rect(313, 310, 400, 95);
		//mLoadButton = new Rect(313, 420, 400, 95);
		//mAboutButton = new Rect(313, 530, 400, 95);
	}
}
