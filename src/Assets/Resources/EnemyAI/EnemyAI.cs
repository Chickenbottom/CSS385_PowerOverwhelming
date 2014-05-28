using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public enum Waypoint
{
    King = 0,
    BottomLeft,
    BottomRight,
    TopLeft,
    TopRight,
    Center,
    CenterLeft,
    CenterRight,
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
    public int CurrentLevel;
    public Era CurrentEra;
    
    private void ValidatePresets ()
    {
        if (CurrentLevel == 0) {// || CurrentErra == Era.None
            Debug.LogError("The level or era was not set in the Unity Inspector.");
        }
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    /////////////////////////////////////////////////////////////////////////////////// 
    
    void Awake ()
    {
        this.ValidatePresets();
        GameState.CurrentEra = this.CurrentEra;
        GameState.CurrentLevel = this.CurrentLevel;
    }
    
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
        
        if (mCurrentWave >= mMaxWaves && units.Count == 0) {
            GameState.TriggerWin ();
        }
        
        /*if (Input.GetButtonDown ("Fire1")) {
            SpawnWave (3);
        }*/
    }
    
    void Start ()
    {
        units = new List<EnemySquad> ();
        
        Waypoints = new Dictionary<Waypoint, Vector3> ();
        
        Waypoints.Add (Waypoint.BottomLeft, GameObject.Find("WP_BottomLeft").transform.position);
        Waypoints.Add (Waypoint.BottomRight, GameObject.Find("WP_BottomRight").transform.position);
        Waypoints.Add (Waypoint.TopRight, GameObject.Find("WP_TopRight").transform.position);
        Waypoints.Add (Waypoint.TopLeft, GameObject.Find("WP_TopLeft").transform.position);
        Waypoints.Add (Waypoint.CenterRight, GameObject.Find("WP_CenterRight").transform.position);
        Waypoints.Add (Waypoint.CenterLeft, GameObject.Find("WP_CenterRight").transform.position);
        Waypoints.Add (Waypoint.King, GameObject.Find("WP_King").transform.position);
        Waypoints.Add (Waypoint.Center, GameObject.Find("WP_Center").transform.position);
        Waypoints.Add (Waypoint.SpawnLeft, GameObject.Find("WP_SpawnLeft").transform.position);
        Waypoints.Add (Waypoint.SpawnCenter, GameObject.Find("WP_SpawnCenter").transform.position);
        Waypoints.Add (Waypoint.SpawnRight, GameObject.Find("WP_SpawnRight").transform.position);
        
        GameObject.Find ("Waypoints").SetActive(false);
        
        Debug.Log ("Loading " + CurrentEra.ToString() + " AI level " + CurrentLevel);
        
        string aiDataPath = "Data/AI/AI_";
        aiDataPath += CurrentEra.ToString ();
        aiDataPath += "_";
        aiDataPath += CurrentLevel.ToString ();
        aiDataPath += ".txt";
        ReadWavesFromFile (aiDataPath);
        
        mCurrentWave = 1;
        this.SpawnWave (mCurrentWave);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // Public Methods and Variables
    /////////////////////////////////////////////////////////////////////////////////// 
    
    public void AddSquad(int size, Vector3 location, UnitType unitType, Vector3? destination = null)
    {
        EnemySquad es = new EnemySquad(size, 0, location, unitType);
        
        if (destination != null)
            es.AddWaypoint(destination.Value);
            
        es.AddWaypoint(Waypoints[Waypoint.King]);
        units.Add(es);
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private Methods and Variables
    /////////////////////////////////////////////////////////////////////////////////// 
    private float mWaveSpawnTime;
    private int mCurrentWave = 0;
    private int mMaxWaves;
    List<string>[] mEnemyWaves;
    float[] mWaveTimers;
    private List<EnemySquad> units;
    private Queue<Vector3> mSpawnPoint;
        
    private void SpawnWave (int waveNumber)
    {   
        if (waveNumber > mMaxWaves)
            return;
        
        Debug.Log ("Spawning Enemy Wave " + waveNumber + " for " + mWaveTimers [waveNumber] + " seconds.");
        GameState.RemainingWaves = mMaxWaves - mCurrentWave;
        
        mWaveSpawnTime = Time.time + mWaveTimers [waveNumber];
        
        foreach (string s in mEnemyWaves[waveNumber])
            this.AddSquad (s);
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
        Vector3 spawnLocation;
        
        char[] delimiters = { ' ', ',' };
        string[] param = input.Split (delimiters, StringSplitOptions.RemoveEmptyEntries);
        
        spawnTime = float.Parse (param [0]);
        size = EnumHelper.FromString<SquadSize> (param [1]);
        Waypoint wp = EnumHelper.FromString<Waypoint> (param [4]);
        spawnLocation = Waypoints[wp];
        //preset = EnumHelper.FromString<SquadPreset> (param [2]);
        //behavior = EnumHelper.FromString<SquadBehavior> (param [3]);
        
        EnemySquad es = new EnemySquad ((int)size, spawnTime, spawnLocation);
        
        for (int i = 5; i < param.Length; ++i) {
            wp = EnumHelper.FromString<Waypoint> (param [i]);
            es.AddWaypoint (Waypoints [wp]);
        }
        es.AddWaypoint (Waypoints [Waypoint.King]);
        
        units.Add (es);
    }
    
    private void ReadWavesFromFile (string filepath)
    {
        StreamReader file = null;
        string input;
        try {
            file = new StreamReader (filepath);
            input = file.ReadLine ();
            mMaxWaves = int.Parse (input);
            mEnemyWaves = new List<string>[mMaxWaves + 1]; // 0 based array
            mWaveTimers = new float[mMaxWaves + 1];
            
        } catch (System.Exception e) {
            Debug.LogError (e.ToString ());
        }
        
        int waveNumber = 0;
        char[] delimiters = { ' ', ',' };
        
        string[] waveData;
        while (true) {
            input = file.ReadLine ();
            if (file.EndOfStream)
                break;
            
            if (input == "" || input [0] == '#') // ignore comments and blank lines
                continue;
                
            // @EnemyWave <WaveNumber> <WaveTimer>
            if (input [0] == '@') { // start of new enemy wave
                waveNumber ++;
                waveData = input.Split (delimiters);
                mWaveTimers [waveNumber] = float.Parse (waveData [1]);
                mEnemyWaves [waveNumber] = new List<string> ();
            } else { // information about the enemy wave
                mEnemyWaves [waveNumber].Add (input);
            }
        }
    }
}