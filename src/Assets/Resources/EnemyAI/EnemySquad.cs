using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class EnemySquad
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Public
    ///////////////////////////////////////////////////////////////////////////////////
    public bool IsDead { get; set; }
    public EnemySquad(int size, Vector3 waypoint) 
    {
        if (mSquadPrefab == null) 
            mSquadPrefab = Resources.Load ("Squads/SquadPrefab") as GameObject;
        
        mCurrentWaypoint = 0;
        
        mSquad = SpawnSquad(size: size);
        mWaypoints = new List<Vector3>();
        this.AddWaypoint(waypoint);
        mSquad.SetDestination(waypoint);
    }
    
    public void AddWaypoint(Vector3 waypoint)
    {
        mWaypoints.Add(waypoint);
    }
    
    public void Update(float deltaTime)
    {
        if (IsDead = (mSquad == null))
            return;
        
        if (mCurrentWaypoint < mWaypoints.Count - 1 && mSquad.IsIdle) {
            mCurrentWaypoint ++;
            mSquad.SetDestination(mWaypoints[mCurrentWaypoint]);
        }
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    ///////////////////////////////////////////////////////////////////////////////////   
	private Vector3 mSpawnLocation;
	private Squad mSquad;
	private int mCurrentWaypoint;
	private List<Vector3> mWaypoints;
	private static GameObject mSquadPrefab;
	
	private Squad SpawnSquad(int size)
	{
		mSpawnLocation = new Vector3(-33f, -60f, 0f);
		GameObject o = (GameObject) GameObject.Instantiate (mSquadPrefab);
		Squad squad = o.GetComponent<Squad> ();
		
		squad.NumSquadMembers = size;
		squad.Spawn (mSpawnLocation, UnitType.Peasant, Allegiance.AI);
		
		return squad;
	}
}
