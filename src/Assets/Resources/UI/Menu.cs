using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Menu : MonoBehaviour 
{
	Dictionary<Button, Rect> buttons;
	
	protected enum Button {
		NewGame,
		LoadGame,
		About
	}
	
	void OnGUI () 
	{
		//GUI.color = Color.clear;
		foreach(Button button in buttons.Keys) {
			if (GUI.Button(buttons[button], ""))
				HandleClick(button);
		}
	}
	
	private void HandleClick(Button button)
	{
		switch(button) 
		{
		case(Button.NewGame):
			Application.LoadLevel("LevelLoader");
			break;
		case(Button.LoadGame):
			break;
		case(Button.About):
			Application.Quit();
			break;
		}
	}
	
	void Awake()
	{
		buttons = new Dictionary<Button, Rect>();

		Rect buttonDimensions = new Rect (313, 310, 400, 95);
		buttons.Add (Button.NewGame, ScaleButton(buttonDimensions));

		buttonDimensions.y += 110; // vertical offset between buttons
		buttons.Add (Button.LoadGame, ScaleButton(buttonDimensions));

		buttonDimensions.y += 110;
		buttons.Add (Button.About, ScaleButton(buttonDimensions));
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
