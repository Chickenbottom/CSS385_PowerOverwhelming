using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerStore : MonoBehaviour {

	protected enum Button {
		kSwordsmanUp,
		kSwordsmanDown,
		kArcherUp,
		kArcherDown,
		kMageUp,
		kMageDown,
		
		kNextLevel,
		kMenu,
	}
	
	protected enum Label {
		kSwordsmanSquadCount,
		kArcherSquadCount,
		kMageSquadCount,
	}
	
	Dictionary<Button, ButtonData> mButtons;
	Dictionary<Label, LabelData> mLabels;
	
	protected delegate int LabelValue();
	
	protected struct LabelData
	{
		public Rect Rect;
		public string Text { get { return mLabelValue.Invoke().ToString(); } }
		
		private LabelValue mLabelValue;
		
		// Pass in a function that is used to fill the text of the label
		public LabelData(Rect r, LabelValue l) 
		{
			Rect = r;
			mLabelValue = l;
		}
	}
	
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
		case (Button.kSwordsmanUp):
			GameState.SwordsmanSquadCount ++;
			break;
		case (Button.kSwordsmanDown):
			GameState.SwordsmanSquadCount --;
			break;
		case (Button.kArcherUp):
			GameState.ArcherSquadCount ++;
			break;
		case (Button.kArcherDown):
			GameState.ArcherSquadCount --;
			break;
		case (Button.kMageUp):
			GameState.MageSquadCount ++;
			break;
		case (Button.kMageDown):
			GameState.MageSquadCount --;
			break;
			
		case (Button.kMenu):
			Application.LoadLevel("Menu");
			break;
			
		case (Button.kNextLevel):
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
		
		foreach(Label label in mLabels.Keys) 
			GUI.Label(mLabels[label].Rect, mLabels[label].Text);
	}
	
	void Awake()
	{
		mButtons = new Dictionary<Button, ButtonData>();
		mLabels = new Dictionary<Label, LabelData>();
		
		mButtons.Add (Button.kSwordsmanUp, 
		              new ButtonData(new Rect(500, 230, 50, 25), "+"));
		mButtons.Add (Button.kSwordsmanDown, 
		              new ButtonData(new Rect(500, 260, 50, 25), "-"));
		mLabels.Add (Label.kSwordsmanSquadCount, new LabelData(
			new Rect(450, 245, 40, 40),
		    delegate { return GameState.SwordsmanSquadCount; } ));
		              
		mButtons.Add (Button.kArcherUp, 
		              new ButtonData(new Rect(500, 350, 50, 25), "+"));
		mButtons.Add (Button.kArcherDown, 
		              new ButtonData(new Rect(500, 380, 50, 25), "-"));              
		mLabels.Add (Label.kArcherSquadCount, new LabelData(
			new Rect(450, 365, 40, 40),
			delegate { return GameState.ArcherSquadCount; } ));
			
		mButtons.Add (Button.kMageUp, 
		              new ButtonData(new Rect(500, 470, 50, 25), "+"));
		mButtons.Add (Button.kMageDown, 
		              new ButtonData(new Rect(500, 500, 50, 25), "-"));
		mLabels.Add (Label.kMageSquadCount, new LabelData(
			new Rect(450, 485, 40, 40),
			delegate { return GameState.MageSquadCount; } ));
			
		mButtons.Add (Button.kMenu,
		              new ButtonData(new Rect(200, 650, 250, 60), "Back to Menu"));
		mButtons.Add (Button.kNextLevel, 
		              new ButtonData(new Rect(500, 650, 250, 60), "Next Level"));
	}
}
