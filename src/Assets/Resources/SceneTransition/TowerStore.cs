using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerStore : MonoBehaviour 
{
	public GUISkin GUISkin;
	
	protected enum Button {
		kSwordsmanUp,
		kSwordsmanDown,
		kArcherUp,
		kArcherDown,
		kMageUp,
		kMageDown,
		
		kNextLevel,
		kLevelSelector,
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
			GameState.UnitSquadCount[UnitType.kSwordsman] ++;
			break;
		case (Button.kSwordsmanDown):
			GameState.UnitSquadCount[UnitType.kSwordsman] --;
			break;
		case (Button.kArcherUp):
			GameState.UnitSquadCount[UnitType.kArcher]  ++;
			break;
		case (Button.kArcherDown):
			GameState.UnitSquadCount[UnitType.kArcher]  --;
			break;
		case (Button.kMageUp):
			GameState.UnitSquadCount[UnitType.kMage]  ++;
			break;
		case (Button.kMageDown):
			GameState.UnitSquadCount[UnitType.kMage]  --;
			break;
			
		case (Button.kLevelSelector):
			Application.LoadLevel("LevelLoader");
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
		
		GUI.skin = this.GUISkin;
		foreach(Label label in mLabels.Keys) 
			GUI.Label(mLabels[label].Rect, mLabels[label].Text);
	}
	
	void Awake()
	{
		mButtons = new Dictionary<Button, ButtonData>();
		mLabels = new Dictionary<Label, LabelData>();
		
		mButtons.Add (Button.kSwordsmanUp, 
		              new ButtonData(ScaleButton(new Rect(500, 230, 50, 25)), "+"));
		mButtons.Add (Button.kSwordsmanDown, 
		              new ButtonData(ScaleButton(new Rect(500, 260, 50, 25)), "-"));
		mLabels.Add (Label.kSwordsmanSquadCount, new LabelData(
			ScaleButton(new Rect(450, 245, 40, 40)),
			delegate { return GameState.UnitSquadCount[UnitType.kSwordsman]; } ));
		              
		mButtons.Add (Button.kArcherUp, 
		              new ButtonData(ScaleButton(new Rect(500, 350, 50, 25)), "+"));
		mButtons.Add (Button.kArcherDown, 
		              new ButtonData(ScaleButton(new Rect(500, 380, 50, 25)), "-"));              
		mLabels.Add (Label.kArcherSquadCount, new LabelData(
			ScaleButton(new Rect(450, 365, 40, 40)),
			delegate { return GameState.UnitSquadCount[UnitType.kArcher]; } ));
			
		mButtons.Add (Button.kMageUp, 
		              new ButtonData(ScaleButton(new Rect(500, 470, 50, 25)), "+"));
		mButtons.Add (Button.kMageDown, 
		              new ButtonData(ScaleButton(new Rect(500, 500, 50, 25)), "-"));
		mLabels.Add (Label.kMageSquadCount, new LabelData(
			ScaleButton(new Rect(450, 485, 40, 40)),
			delegate { return GameState.UnitSquadCount[UnitType.kMage]; } ));
			
		mButtons.Add (Button.kLevelSelector,
		              new ButtonData(ScaleButton(new Rect(200, 650, 250, 60)), "Back to Level Selection"));
		mButtons.Add (Button.kNextLevel, 
		              new ButtonData(ScaleButton(new Rect(500, 650, 250, 60)), "Next Level"));
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
