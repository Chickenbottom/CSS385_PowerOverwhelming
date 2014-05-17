using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Waypoint
{
    King,
    ArcherTower,
    AbilityTower,
    SwordsmanTower,
    MageTower,
    Center,
    LowerLeft,
    LowerCenter,
    LowerRight,
    SpawnPoint,
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
    public int NumWaves = 3;
	
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////	
	
    void Update ()
    {
        if (mWaveSpawnInterval < Time.time - mLastEnemySpawn) {
            mLastEnemySpawn = Time.time;
            this.SpawnWave ();
        }
		
        for (int i = units.Count - 1; i >= 0; --i) {
            if (units [i].IsDead)
                units.RemoveAt (i);
            else 
                units [i].Update (Time.deltaTime);
        }
		
        if (mCurrentWave >= NumWaves && units.Count == 0) {
            GameState.TriggerWin ();
        }
		
        if (Input.GetButtonDown ("Fire1")) {
            SpawnWave2 ();
        }
    }
	
    void Start ()
    {
        mLastEnemySpawn = Time.time;
        units = new List<EnemySquad> ();
		
        Waypoints = new Dictionary<Waypoint, Vector3> ();
        Waypoints.Add (Waypoint.AbilityTower, new Vector3 (43, -45, 0));
        Waypoints.Add (Waypoint.ArcherTower, new Vector3 (-105, -45, 0));
        Waypoints.Add (Waypoint.MageTower, new Vector3 (43, 50, 0));
        Waypoints.Add (Waypoint.SwordsmanTower, new Vector3 (-108, 50, 0));
        Waypoints.Add (Waypoint.King, new Vector3 (-33, 56, 0));
        Waypoints.Add (Waypoint.Center, new Vector3 (-33, 10, 0));
        Waypoints.Add (Waypoint.LowerLeft, new Vector3 (-44, -37, 0));
        Waypoints.Add (Waypoint.LowerCenter, new Vector3 (-33, -37, 0));
        Waypoints.Add (Waypoint.LowerRight, new Vector3 (-22, -37, 0));
        Waypoints.Add (Waypoint.SpawnPoint, new Vector3 (-33, -64, 0));
		
        SpawnWave ();
        
        this.AddSquad ("Large Default 3.5 Center,King");
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // Public Methods and Variables
    ///////////////////////////////////////////////////////////////////////////////////	
	
	
    ///////////////////////////////////////////////////////////////////////////////////
    // Private Methods and Variables
    ///////////////////////////////////////////////////////////////////////////////////	
    private float mWaveSpawnInterval = 15.0f;
    private float mLastEnemySpawn;
    private int mCurrentWave = 0;
	
    private List<EnemySquad> units;
	    
    // Format: <Size> <Preest> <Spawn Time> <Action Type> <Waypoint>,<Waypoint>,...
    // "Large Default 2.5 Move ArcherTower,SwordsmanTower"
    // "Individual Elite 1 ForcedMove AbilityTower"
    private void AddSquad (string input)
    {
        SquadPreset preset;
        SquadSize size;
        float spawnTime;
        
        char[] delimiters = { ' ', ',' };
        string[] param = input.Split (delimiters);
        
        size = EnumHelper.FromString<SquadSize> (param [0]);
        preset = EnumHelper.FromString<SquadPreset> (param [1]);
        spawnTime = float.Parse (param [2]);
        
        EnemySquad es = new EnemySquad ((int)size, spawnTime);
        
        for (int i = 3; i < param.Length; ++i) {
            Waypoint wp = EnumHelper.FromString<Waypoint> (param [i]);
            es.AddWaypoint (Waypoints [wp]);
        }
        es.AddWaypoint (Waypoints [Waypoint.King]);
        
        units.Add (es);
    }
    
    private void SpawnWave ()
    {
        mCurrentWave ++;
        switch (mCurrentWave) {
        case(1):
            SpawnWave1 ();
            break;
        case(2):
            //SpawnWave2 ();
            break;
        case(3):
            //SpawnWave3 ();
            break;
        }
    }
    
    // TODO fix this hard coding of enemy waves
    void SpawnWave1 ()
    {
        this.AddSquad ("Large Default 0 LowerLeft,ArcherTower");
        this.AddSquad ("Medium Default 1");
        this.AddSquad ("Large Default 3 LowerRight,AbilityTower");
        this.AddSquad ("Medium Default 4");
        this.AddSquad ("Medium Default 5 LowerRight,MageTower");
        this.AddSquad ("Large Default 6 LowerLeft,ArcherTower");
        this.AddSquad ("Large Default 7");
    }
	
    void SpawnWave2 ()
    {
        this.AddSquad ("Individual Default 0 LowerLeft,ArcherTower,SwordsmanTower");
        this.AddSquad ("Individual Default 0 LowerLeft,ArcherTower,SwordsmanTower");
        this.AddSquad ("Individual Default 0 LowerLeft,ArcherTower,SwordsmanTower");
        
        this.AddSquad ("Large Default 1 LowerLeft,ArcherTower,SwordsmanTower");
        this.AddSquad ("Large Default 2 LowerCenter,Center,MageTower");
        this.AddSquad ("Large Default 6 LowerCenter,SwordsmanTower");
        this.AddSquad ("Large Default 7 LowerRight,MageTower");
    }
	
    void SpawnWave3 ()
    {
        this.AddSquad ("Medium Default 0 LowerLeft,ArcherTower");
        this.AddSquad ("Medium Default 1 LowerRight,AbilityTower");
        this.AddSquad ("Medium Default 2 LowerCenter,MageTower");
        this.AddSquad ("Medium Default 3 Center,SwordsmanTower");
        this.AddSquad ("Medium Default 4 LowerLeft,ArcherTower");
        this.AddSquad ("Medium Default 5 LowerRight,AbilityTower");
        this.AddSquad ("Medium Default 6 LowerCenter,MageTower");
        this.AddSquad ("Medium Default 7 Center,SwordsmanTower");
    }
}