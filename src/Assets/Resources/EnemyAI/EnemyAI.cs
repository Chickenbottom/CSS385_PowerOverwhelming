using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public enum Waypoint
{
    King = 0,
    TowerLeft,
    TowerRight,
    SwordsmanTower,
    MageTower,
    Center,
    CenterLeft,
    CenterRight,
    Left,
    Middle,
    Right,
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
        
        if (mCurrentWave >= mMaxWaves && units.Count == 0) {
            GameState.TriggerWin ();
        }
        
        if (Input.GetButtonDown ("Fire1")) {
            SpawnWave (3);
        }
    }
    
    void Awake ()
    {
        units = new List<EnemySquad> ();
        
        // TODO grab these locations from the map
        Waypoints = new Dictionary<Waypoint, Vector3> ();
        Waypoints.Add (Waypoint.TowerRight, GameObject.Find("TowerRight").transform.position);
        Waypoints.Add (Waypoint.TowerLeft, GameObject.Find("TowerLeft").transform.position);
        Waypoints.Add (Waypoint.MageTower, new Vector3 (43, 50, 0));
        Waypoints.Add (Waypoint.SwordsmanTower, new Vector3 (-108, 50, 0));
        Waypoints.Add (Waypoint.King, GameObject.Find("KingWP").transform.position);
        Waypoints.Add (Waypoint.Center, GameObject.Find("CenterWP").transform.position);
        Waypoints.Add (Waypoint.CenterLeft, new Vector3 (-93, 0, 0));
        Waypoints.Add (Waypoint.CenterRight, new Vector3 (-6, 0, 0));
        Waypoints.Add (Waypoint.Left, GameObject.Find("SpawnLeft").transform.position);
        Waypoints.Add (Waypoint.Middle, GameObject.Find("SpawnCenter").transform.position);
        Waypoints.Add (Waypoint.Right, GameObject.Find("SpawnRight").transform.position);
        
        GameObject.Find ("Waypoints").SetActive(false);
        
        mSpawnPoint = new Queue<Vector3>();
        mSpawnPoint.Enqueue(Waypoints[Waypoint.Left]);
        mSpawnPoint.Enqueue(Waypoints[Waypoint.Middle]);
        mSpawnPoint.Enqueue(Waypoints[Waypoint.Right]);
        
        string aiData = "AI_Level";
        aiData += GameState.CurrentLevel.ToString ();
        aiData += ".txt";
        ReadWavesFromFile (aiData);
        
        mCurrentWave = 1;
        this.SpawnWave (mCurrentWave);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // Public Methods and Variables
    /////////////////////////////////////////////////////////////////////////////////// 
    
    public void AddSquad(Vector3 location)
    {
        EnemySquad es = new EnemySquad(2, 0, location);
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