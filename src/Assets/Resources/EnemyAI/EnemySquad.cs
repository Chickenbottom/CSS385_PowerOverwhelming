using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class EnemySquad 
{
	private Vector3 spawnLocation;
	private Squad squad;
	private int currentWaypoint;
	private List<Vector3> waypoints;
	private static GameObject squadPrefab;
	
	public EnemySquad(int size, Vector3 waypoint) 
	{
		if (squadPrefab == null) 
			squadPrefab = Resources.Load ("Squads/SquadPrefab") as GameObject;
		
		currentWaypoint = 0;
		
		squad = CreateSquad(size: size);
		waypoints = new List<Vector3>();
		this.AddWaypoint(waypoint);
		squad.SetDestination(waypoint);
	}
	
	public void AddWaypoint(Vector3 waypoint)
	{
		waypoints.Add(waypoint);
	}
	
	Squad CreateSquad(int size)
	{
		spawnLocation = new Vector3(-33f, -60f, 0f);
		GameObject o = (GameObject) GameObject.Instantiate (squadPrefab);
		Squad squad = o.GetComponent<Squad> ();
		
		squad.NumSquadMembers = size;
		squad.Spawn (spawnLocation, UnitType.Peasant, Allegiance.AI);
		
		return squad;
	}
	
	public bool IsDead { get; set; }
	
	public void Update()
	{			
		if (squad == null)
			IsDead = true;
			
		if (currentWaypoint < waypoints.Count - 1 && squad.IsIdle) {
			currentWaypoint ++;
			squad.SetDestination(waypoints[currentWaypoint]);
		}
	}
}
