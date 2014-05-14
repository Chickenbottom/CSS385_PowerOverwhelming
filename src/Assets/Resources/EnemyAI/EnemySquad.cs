using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class EnemySquad 
{
	private Vector3 mSpawnLocation;
	private Squad mSquad;
	private int mCurrentWaypoint;
	private List<Vector3> mWaypoints;
	private static GameObject mSquadPrefab;
	
	public EnemySquad(int size, Vector3 waypoint) 
	{
		if (mSquadPrefab == null) 
			mSquadPrefab = Resources.Load ("Squads/SquadPrefab") as GameObject;
		
		mCurrentWaypoint = 0;
		
		
		mSquad = CreateSquad(size: size);
		mWaypoints = new List<Vector3>();
		this.AddWaypoint(waypoint);
		mSquad.UpdateSquadDestination(waypoint);
	}
	
	public void AddWaypoint(Vector3 waypoint)
	{
		mWaypoints.Add(waypoint);
	}
	
	Squad CreateSquad(int size)
	{
		mSpawnLocation = new Vector3(-33f, -60f, 0f);
		GameObject o = (GameObject) GameObject.Instantiate (mSquadPrefab);
		Squad squad = o.GetComponent<Squad> ();
		
		squad.NumSquadMembers = size;
		squad.Spawn (mSpawnLocation, UnitType.kPeasant, Allegiance.kAI);
		
		return squad;
	}
	
	public void Update()
	{
		if (mCurrentWaypoint < mWaypoints.Count - 1 && mSquad.IsIdle) {
			mCurrentWaypoint ++;
			mSquad.UpdateSquadDestination(mWaypoints[mCurrentWaypoint]);
		}
	}
}
