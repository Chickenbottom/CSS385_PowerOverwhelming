using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class EnemySquad
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Public
    ///////////////////////////////////////////////////////////////////////////////////
    public bool IsDead { get; set; }

    public EnemySquad (int size, float spawnTime = 0f)
    {
        if (mSquadPrefab == null) 
            mSquadPrefab = Resources.Load ("Squads/SquadPrefab") as GameObject;
        
        if (mSpawnLocation == null) {
            mSpawnLocation = GameObject.Find ("EnemySpawnPoint").transform.position;
        }

        mSquadSize = size;
        mSpawnTime = spawnTime;
        
        mWaypoints = new List<Vector3> ();
    }
    
    public void AddWaypoint (Vector3 waypoint)
    {
        mWaypoints.Add (waypoint);
    }
    
    public void Update (float deltaTime)
    {
        if (IsDead = (mSpawnTime < 0 && mSquad == null))
            return;
        
        mSpawnTime -= deltaTime; // update spawn timer
        if (mSquad == null && mSpawnTime < 0)
            this.SpawnSquad (mSquadSize);
        
        UpdateDestination ();
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    ///////////////////////////////////////////////////////////////////////////////////
    private static GameObject mSquadPrefab;
    private static Vector3? mSpawnLocation;
    private Squad mSquad;
    private List<Vector3> mWaypoints;
    private float mSpawnTime; // time before squad spawns
        
    private int mCurrentWaypoint;
    private int mSquadSize;
    
    private void UpdateDestination ()
    {
        if (mSquad == null) // squad has not spawned yet
            return;
              
        if (mCurrentWaypoint < mWaypoints.Count - 1 && mSquad != null && mSquad.IsIdle) {
            mCurrentWaypoint ++;
            mSquad.SetDestination (mWaypoints [mCurrentWaypoint]);
            //Debug.Log ("Sending " + mSquad + " to " + mWaypoints[mCurrentWaypoint]);
        }
    }
    
    private void SpawnSquad (int size)
    {
        GameObject o = (GameObject)GameObject.Instantiate (mSquadPrefab);
        Squad squad = o.GetComponent<Squad> ();
        
        squad.NumSquadMembers = size;
        squad.Spawn (mSpawnLocation.Value, UnitType.Peasant, Allegiance.AI);
        
        mSquad = squad;
        mSquad.SetDestination (mWaypoints [0]);
    }
}
