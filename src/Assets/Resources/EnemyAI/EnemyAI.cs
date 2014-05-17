using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public enum Waypoint
{
    King,
    ArcherTower,
    AbilityTower,
    SwordsmanTower,
    MageTower,
    Center,
    SpawnLeft,
    SpawnCenter,
    SpawnRight,
}

public enum SquadPreset
{
    Default, // all peasants
    Elite,   // with an elite
}

public enum SquadSize
{
    Individual = 1,
    Small = 2,
    Medium = 4,
    Large = 7,
}

public enum SquadBehavior
{
    AttackMove,
    ForceMove,
}

public static class EnumHelper
{    
    public static T FromString<T> (string value)
    {
        return (T)Enum.Parse (typeof(T), value);
    }
}

public class EnemyAI : MonoBehaviour
{
    public Dictionary<Waypoint, Vector3> Waypoints;
	
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////	
	
    void Update ()
    {
        if (mCurrentWave <= mMaxWaves && Time.time > mWaveSpawnTime) {
            mCurrentWave ++;
            this.SpawnWave (mCurrentWave);
        }
		
        for (int i = units.Count - 1; i >= 0; --i) {
            if (units [i].IsDead)
                units.RemoveAt (i);
            else 
                units [i].Update (Time.deltaTime);
        }
		
        if (mCurrentWave > mMaxWaves && units.Count == 0) {
            GameState.TriggerWin ();
        }
		
        if (Input.GetButtonDown ("Fire1")) {
            SpawnWave (3);
        }
    }
	
    void Start ()
    {
        units = new List<EnemySquad> ();
		
        // TODO grab these locations from the map
        Waypoints = new Dictionary<Waypoint, Vector3> ();
        Waypoints.Add (Waypoint.AbilityTower, new Vector3 (43, -45, 0));
        Waypoints.Add (Waypoint.ArcherTower, new Vector3 (-105, -45, 0));
        Waypoints.Add (Waypoint.MageTower, new Vector3 (43, 50, 0));
        Waypoints.Add (Waypoint.SwordsmanTower, new Vector3 (-108, 50, 0));
        Waypoints.Add (Waypoint.King, new Vector3 (-33, 56, 0));
        Waypoints.Add (Waypoint.Center, new Vector3 (-33, 10, 0));
        Waypoints.Add (Waypoint.SpawnLeft, new Vector3 (-44, -37, 0));
        Waypoints.Add (Waypoint.SpawnCenter, new Vector3 (-33, -37, 0));
        Waypoints.Add (Waypoint.SpawnRight, new Vector3 (-22, -37, 0));
		
        string aiData = "Assets/Resources/EnemyAI/AI_Level";
        aiData += GameState.CurrentLevel.ToString();
        aiData += ".txt";
        ReadWavesFromFile(aiData);
        
        mCurrentWave = 1;
        this.SpawnWave(mCurrentWave);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // Public Methods and Variables
    ///////////////////////////////////////////////////////////////////////////////////	
	
	
    ///////////////////////////////////////////////////////////////////////////////////
    // Private Methods and Variables
    ///////////////////////////////////////////////////////////////////////////////////	
    private float mWaveSpawnTime;
    private int mCurrentWave = 0;
    
    private int mMaxWaves;
    List<string>[] mEnemyWaves;
    float[] mWaveTimers;
    
    private List<EnemySquad> units;
	    
    private void SpawnWave (int waveNumber)
    {   
        if (waveNumber > mMaxWaves)
            return;
        
        Debug.Log ("Spawning Enemy Wave " + waveNumber + " for " + mWaveTimers[waveNumber] + " seconds.");
        mWaveSpawnTime = Time.time + mWaveTimers[waveNumber];
        
        foreach (string s in mEnemyWaves[waveNumber])
            this.AddSquad(s);
    }
    
    // Format: <Spawn Time> <Size> <Preset> <Action Type> <Waypoint>,<Waypoint>,...
    // "2.5 Large Default AttackMove ArcherTower,SwordsmanTower"
    // "1 Individual Elite ForcedMove AbilityTower"
    private void AddSquad (string input)
    {
        SquadSize size;
        //SquadPreset preset;
        //SquadBehavior behavior;
        
        float spawnTime;
        
        char[] delimiters = { ' ', ',' };
        string[] param = input.Split (delimiters, StringSplitOptions.RemoveEmptyEntries);
        
        spawnTime = float.Parse (param [0]);
        size = EnumHelper.FromString<SquadSize> (param [1]);
        //preset = EnumHelper.FromString<SquadPreset> (param [2]);
        //behavior = EnumHelper.FromString<SquadBehavior> (param [3]);
        
        EnemySquad es = new EnemySquad ((int)size, spawnTime);
        
        for (int i = 4; i < param.Length; ++i) {
            Waypoint wp = EnumHelper.FromString<Waypoint> (param [i]);
            es.AddWaypoint (Waypoints [wp]);
        }
        es.AddWaypoint (Waypoints [Waypoint.King]);
        
        units.Add (es);
    }
    
    private void ReadWavesFromFile(string filepath)
    {
        StreamReader file = null;
        string input;
        try {
            file = new StreamReader(filepath);
            input = file.ReadLine();
            mMaxWaves = int.Parse (input);
            mEnemyWaves = new List<string>[mMaxWaves + 1]; // 0 based array
            mWaveTimers = new float[mMaxWaves + 1];
            
        } catch (System.Exception e) {
            Debug.LogError(e.ToString());
        }
        
        int waveNumber = 0;
        char[] delimiters = { ' ', ',' };
        
        string[] waveData;
        while(true) {
            input = file.ReadLine();
            if(file.EndOfStream)
                break;
            
            if (input == "" || input[0] == '#') // ignore comments and blank lines
                continue;
                
            // @EnemyWave <WaveNumber> <WaveTimer>
            if(input[0] == '@') { // start of new enemy wave
                waveNumber ++;
                waveData = input.Split(delimiters);
                mWaveTimers[waveNumber] = float.Parse(waveData[1]);
                mEnemyWaves[waveNumber] = new List<string>();
            } else { // information about the enemy wave
                mEnemyWaves[waveNumber].Add(input);
            }
        }
    }
}