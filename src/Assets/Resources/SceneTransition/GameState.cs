using UnityEngine;
using System.Collections;

public class GameState {
	private static int mGameLevel;
	private static int[] mNumEnemies;
	
	public static int Score { get { return mScore; } }
	private static int mScore = 0;
	
	public static int MageSquadCount { get; set; }
	public static int SwordsmanSquadCount { get; set; }
	public static int ArcherSquadCount { get; set; }
	
	static GameState()
	{
		MageSquadCount = 4;
		SwordsmanSquadCount = 4;
		ArcherSquadCount = 4;
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

