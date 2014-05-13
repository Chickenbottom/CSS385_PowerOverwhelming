using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Menu : MonoBehaviour 
{
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

		Rect buttonDimensions = new Rect (313, 310, 400, 95);
		mButtons.Add (Button.kNewGame, ScaleButton(buttonDimensions));

		buttonDimensions.y += 110; // vertical offset between buttons
		mButtons.Add (Button.kLoadGame, ScaleButton(buttonDimensions));

		buttonDimensions.y += 110;
		mButtons.Add (Button.kAbout, ScaleButton(buttonDimensions));
	}

	// TODO make this a general utility function
	// TODO add these to global game state
	float kScreenWidth = 1024;
	float kScreenHeight = 768;
	
	Rect ScaleButton(Rect button)
	{
		float widthRatio = Screen.width / kScreenWidth;
		float heightRatio = Screen.height / kScreenHeight;
		
		button.x *= widthRatio;
		button.width *= widthRatio;
		button.y *= heightRatio;
		button.height *= heightRatio;
		
		return button;
	}
}
