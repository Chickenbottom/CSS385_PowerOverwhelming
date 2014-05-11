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
		mButtons.Add (Button.kTowerStore, 
		              new ButtonData(new Rect(313, 310, 400, 95), "Galactic Tower Store"));
		mButtons.Add (Button.kMenu, 
		              new ButtonData(new Rect(313, 420, 400, 95), "Back to Menu"));
		
		mLevelButtons = new List<Rect>();
		for (int i = 0; i < 5; ++i)
			mLevelButtons.Add(new Rect(313 + 80 * i, 550, 75, 75));
	}
}
