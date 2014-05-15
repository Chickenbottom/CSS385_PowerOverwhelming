using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour 
{
	protected enum Button {
		kTowerStore,
		kMenu
	}
	
	List<Rect> mLevelButtons;
	Dictionary<Button, ButtonData> mButtons;
	
	protected struct ButtonData
	{
		public Rect rect;
		public string text;
		
		public ButtonData(Rect r, string s) 
		{
			rect = r;
			text = s;
		}
	}
	
	private void HandleClick(Button button)
	{
		switch(button) 
		{
		case(Button.kTowerStore):
			Application.LoadLevel("TowerStore");
			break;
		case(Button.kMenu):
			Application.LoadLevel("Menu");
			break;
		}
	}
	
	private void LoadLevel(int level)
	{
		GameState.LoadLevel(level);
	}
	
	void OnGUI () 
	{
		foreach(Button button in mButtons.Keys) {
			if (GUI.Button(mButtons[button].rect, mButtons[button].text))
				HandleClick(button);
		}
		
		for (int i = 0; i < mLevelButtons.Count; ++i)
			if (GUI.Button(mLevelButtons[i], i.ToString()))
				LoadLevel(i);
	}
	
	void Awake()
	{
		mButtons = new Dictionary<Button, ButtonData>();

		Rect buttonRect = new Rect (313, 310, 400, 95);
		mButtons.Add (Button.kTowerStore, 
		              new ButtonData(ScaleButton(buttonRect), "Galactic Tower Store"));

		buttonRect.y += 110; // vertical offset between buttons
		mButtons.Add (Button.kMenu, 
		              new ButtonData(ScaleButton(buttonRect), "Back to Menu"));

		Rect levelButton = new Rect (233, 550, 75, 75);
		mLevelButtons = new List<Rect>();
		for (int i = 0; i < 5; ++i) {
			levelButton.x += 80; // offset between buttons
			mLevelButtons.Add (ScaleButton(levelButton));
		}
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
