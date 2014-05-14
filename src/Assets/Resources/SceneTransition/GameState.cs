using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState {
	private static int mGameLevel;
	private static int[] mNumEnemies;
	
	public static int Score { get { return mScore; } }
	private static int mScore = 0;
	
	public static Dictionary<UnitType, int> UnitSquadCount { get; set; }
	public static Dictionary<UnitType, int> UnitExperience { get; set; }
	
	static GameState()
	{
		UnitSquadCount = new Dictionary<UnitType, int>();
		UnitSquadCount.Add(UnitType.kArcher, 4);
		UnitSquadCount.Add(UnitType.kSwordsman, 3);
		UnitSquadCount.Add(UnitType.kMage, 1);
		
		UnitExperience = new Dictionary<UnitType, int>();
		UnitExperience.Add(UnitType.kArcher, 0);
		UnitExperience.Add(UnitType.kSwordsman, 0);
		UnitExperience.Add(UnitType.kMage, 0);
	}
	
	public static void AddExperience(UnitType unitType, int exp)
	{
		if(!UnitExperience.ContainsKey(unitType))
			return;
		
		UnitExperience[unitType] += exp;
	}
	
	public static void AddToScore(int value)
	{
		mScore += value;
	}
	
	public static void LoadLevel(int level)
	{
		//mGameLevel = level;
		Application.LoadLevel("Level"+level.ToString());
	}
	
	public static void TriggerLoss()
	{
		Application.LoadLevel ("LevelLoader");
	}
	
	public static void TriggerWin()
	{
		GUIText text = GameObject.Find ("DialogueLeft").GetComponent<GUIText> ();
		text.text = "The peasants are gone!";
	}
}

