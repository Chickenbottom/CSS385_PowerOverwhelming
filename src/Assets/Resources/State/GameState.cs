using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState {
	private static int gameLevel;
	private static int[] numEnemies;

    public static int Score { get; set; }
	private static int score = 0;

    public static Dictionary<UnitType, int> UnitSquadCount { get; set; }
	public static Dictionary<UnitType, int> UnitExperience { get; set; }
	
	static GameState()
	{
		UnitSquadCount = new Dictionary<UnitType, int>();
		UnitSquadCount.Add(UnitType.Archer, 4);
		UnitSquadCount.Add(UnitType.Swordsman, 3);
		UnitSquadCount.Add(UnitType.Mage, 1);
		
		UnitExperience = new Dictionary<UnitType, int>();
		UnitExperience.Add(UnitType.Archer, 0);
		UnitExperience.Add(UnitType.Swordsman, 0);
		UnitExperience.Add(UnitType.Mage, 0);
	}
	
	public static void AddExperience(UnitType unitType, int exp)
	{
		if(!UnitExperience.ContainsKey(unitType))
			return;
		
		UnitExperience[unitType] += exp;
	}
	
	public static void AddToScore(int value)
	{
		score += value;
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

