﻿using UnityEngine;
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
		mSpawnLocation = new Vector3(0f, -100f, 0f);
		
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
		GameObject o = (GameObject) GameObject.Instantiate (mSquadPrefab);
		Squad squad = o.GetComponent<Squad> ();
		
		squad.NumSquadMembers = size;
		squad.Spawn (mSpawnLocation, UnitType.kPeasant);
		
		return squad;
	}
}
