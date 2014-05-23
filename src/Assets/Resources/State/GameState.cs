using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState
{
    public static Era CurrentEra { get; set; }

    public static int CurrentLevel;
    public static int RemainingWaves;
	public static bool WonGame = false;
	public static bool LostGame = false;

    public static int Gold;
        
    private static int[] numEnemies;

    public static int KingsHealth { 
        get { return mKingsHealth; }
        set { UpdateKingsHealth(value); }
    }

    public static Dictionary<UnitType, int> UnitSquadCount { get; set; }

    public static Dictionary<UnitType, int> RequiredUnitExperience { get; set; }

    public static Dictionary<UnitType, float> SpawnTimes { get; set; }
    
    static GameState ()
    {
        // TODO pull these values from a file
        UnitSquadCount = new Dictionary<UnitType, int> ();
        UnitSquadCount.Add (UnitType.Archer, 3);
        UnitSquadCount.Add (UnitType.Swordsman, 3);
        UnitSquadCount.Add (UnitType.Mage, 1);
        
        RequiredUnitExperience = new Dictionary<UnitType, int> ();
        RequiredUnitExperience.Add (UnitType.Archer, 20);
        RequiredUnitExperience.Add (UnitType.Swordsman, 20);
        RequiredUnitExperience.Add (UnitType.Mage, 20);
        
        SpawnTimes = new Dictionary<UnitType, float> ();
        SpawnTimes.Add (UnitType.Archer, 10f);
        SpawnTimes.Add (UnitType.Swordsman, 10f);
        SpawnTimes.Add (UnitType.Mage, 20f);
        
        Gold = 0;
    }
    
    public static void LoadLevel (int level)
    {
        //mGameLevel = level;
        Application.LoadLevel ("Level" + level.ToString ());
    }
    
    public static void TriggerLoss ()
    {
		LostGame = true;
        //GameObject.Find("LoseFrame").GetComponent<LoseGame>().LostGame();
        //Application.LoadLevel("LevelLoader");
    }
    
    public static void TriggerWin ()
    {
		WonGame = true;
        //GameObject.Find("WinFrame").GetComponent<WinGame>().WonGame();
        //Debug.Log ("You win!");
        //GUIText text = GameObject.Find("DialogueLeft").GetComponent<GUIText>();
        //text.text = "The peasants are gone!";
        //Application.LoadLevel("LevelLoader");
    }
    
    public static void UpdateKingsHealth (int value)
    {
        mKingsHealth = value;
        float maxHealth = UnitUpgrades.GetStat(UnitType.King, UnitStat.Health);
        
        if (mKingsHealth < (int)(0.75 * maxHealth))
            GameObject.Find("Dialogue").GetComponent<DialogueManager>().TriggerDialogue("KingDamaged");
        
        if (mKingsHealth < (int)(0.35 * maxHealth))
            GameObject.Find("Dialogue").GetComponent<DialogueManager>().TriggerDialogue("KingInjured");
        
        if (mKingsHealth <= 0)
            TriggerLoss ();
        else if (CurrentEra < Era.Future) {
            CurrentEra++;
        }
    }
    
    private static int mKingsHealth;
}
