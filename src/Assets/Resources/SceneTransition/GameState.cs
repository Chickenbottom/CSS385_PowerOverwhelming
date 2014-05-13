using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState {
	private static int mGameLevel;
	private static int[] mNumEnemies;
	
	public static int Score { get { return mScore; } }
	private static int mScore = 0;
	
	public static Dictionary<UnitType, int> UnitSquadCount { get; set; }
	
	static GameState()
	{
		UnitSquadCount = new Dictionary<UnitType, int>();
		UnitSquadCount.Add(UnitType.kArcher, 4);
		UnitSquadCount.Add(UnitType.kSwordsman, 3);
		UnitSquadCount.Add(UnitType.kMage, 1);
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
}

