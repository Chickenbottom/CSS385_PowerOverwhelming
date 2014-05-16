using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState {
	private static int gameLevel;
	private static int[] numEnemies;

	public static int KingsHealth { 
		get { return mKingsHealth; }
		set { 
			mKingsHealth = value; 
			CheckLoseCondition();
		} 
	}

    public static Dictionary<UnitType, int> UnitSquadCount { get; set; }
	public static Dictionary<UnitType, int> UnitExperience { get; set; }
	public static Dictionary<UnitType, int> RequiredUnitExperience { get; set; }
	public static Dictionary<UnitType, float> SpawnTimes { get; set; }
	
	static GameState()
	{
		// TODO pull these values from a file
		UnitSquadCount = new Dictionary<UnitType, int>();
		UnitSquadCount.Add(UnitType.Archer, 2);
		UnitSquadCount.Add(UnitType.Swordsman, 4);
		UnitSquadCount.Add(UnitType.Mage, 1);
		
		UnitExperience = new Dictionary<UnitType, int>();
		UnitExperience.Add(UnitType.Archer, 0);
		UnitExperience.Add(UnitType.Swordsman, 0);
		UnitExperience.Add(UnitType.Mage, 0);
		
		RequiredUnitExperience = new Dictionary<UnitType, int>();
		RequiredUnitExperience.Add(UnitType.Archer, 20);
		RequiredUnitExperience.Add(UnitType.Swordsman, 20);
		RequiredUnitExperience.Add(UnitType.Mage, 20);
		
		SpawnTimes = new Dictionary<UnitType, float>();
		SpawnTimes.Add(UnitType.Archer, 18f);
		SpawnTimes.Add(UnitType.Swordsman, 5f);
		SpawnTimes.Add(UnitType.Mage, 25f);
	}
	
	public static void AddExperience(UnitType unitType, int exp)
	{
		if(!UnitExperience.ContainsKey(unitType))
			return;
		
		UnitExperience[unitType] += exp;
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
	
	public static void CheckLoseCondition()
	{
		if (mKingsHealth <= 0)
			TriggerLoss ();
	}
	
	private static int mKingsHealth;
}

