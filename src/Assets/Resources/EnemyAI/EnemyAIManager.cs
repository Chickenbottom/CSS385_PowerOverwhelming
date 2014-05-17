using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIManager : MonoBehaviour
{

	private enum Waypoints {
		King,
		ArcherTower,
		AbilityTower,
		SwordsmanTower,
		MageTower,
		Center,
		LowerCenter,
		SpawnPoint,
	}
	private Dictionary<Waypoints, Vector3> wayPoints;
	
	
	public int numWaves = 3;
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	///////////////////////////////////////////////////////////////////////////////////	
	
	void Update()
	{
		if (waveSpawnInterval < Time.time - lastEnemySpawn) {
			lastEnemySpawn = Time.time;
			this.SpawnWave();
		}
		
		for (int i = units.Count - 1; i >= 0; --i) {
			if (units[i].IsDead)
				units.RemoveAt(i);
			else 
				units[i].Update(Time.deltaTime);
		}
		
		if (currentWave >= numWaves && units.Count == 0) {
			GameState.TriggerWin();
		}
		
		if (Input.GetButtonDown("Fire1")) {
			SpawnWave3 ();
		}
	}
	
	void Start()
	{
		lastEnemySpawn = Time.time;
		units = new List<EnemySquad> ();
		
		wayPoints = new Dictionary<Waypoints, Vector3>();
		wayPoints.Add (Waypoints.AbilityTower, new Vector3(43, -40, 0));
		wayPoints.Add (Waypoints.ArcherTower, new Vector3(-105, -40, 0));
		wayPoints.Add (Waypoints.MageTower, new Vector3(43, 50, 0));
		wayPoints.Add (Waypoints.SwordsmanTower, new Vector3(-108, 50, 0));
		wayPoints.Add (Waypoints.King, new Vector3(-33, 56, 0));
		wayPoints.Add (Waypoints.Center, new Vector3(-33, 10, 0));
		wayPoints.Add (Waypoints.LowerCenter, new Vector3(-33, -37, 0));
		wayPoints.Add (Waypoints.SpawnPoint, new Vector3(-33, -64, 0));
		
		SpawnWave();
	}

	///////////////////////////////////////////////////////////////////////////////////
	// Public Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////	
	
	
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////	
	private float waveSpawnInterval = 15.0f;
	private float lastEnemySpawn;
	private int currentWave = 0;
	
	private List<EnemySquad> units;
	
	private void SpawnWave()
	{
		currentWave ++;
		switch (currentWave)
		{
		case(1):
			SpawnWave1();
			break;
		case(2):
			SpawnWave2();
			break;
		case(3):
			SpawnWave3();
			break;
		}
	}
	
	// TODO fix this hard coding of enemy waves
	void SpawnWave1()
	{
		EnemySquad es = null;
		es = new EnemySquad (4, wayPoints[Waypoints.LowerCenter]);
		es.AddWaypoint(wayPoints[Waypoints.King]);
		units.Add (es);
		
		es = new EnemySquad (6, wayPoints[Waypoints.LowerCenter]);
		es.AddWaypoint(wayPoints[Waypoints.ArcherTower]);
		es.AddWaypoint(wayPoints[Waypoints.King]);
		units.Add (es);
	}
	
	void SpawnWave2()
	{
		EnemySquad es = null;
		es = new EnemySquad (5, wayPoints[Waypoints.LowerCenter]);
		es.AddWaypoint(wayPoints[Waypoints.ArcherTower]);
		es.AddWaypoint(wayPoints[Waypoints.King]);
		units.Add (es);
		
		es = new EnemySquad (5, wayPoints[Waypoints.Center]);
		es.AddWaypoint(wayPoints[Waypoints.MageTower]);
		es.AddWaypoint(wayPoints[Waypoints.King]);
		units.Add (es);
	}
	
	void SpawnWave3()
	{
		EnemySquad es = null;
		es = new EnemySquad (7, wayPoints[Waypoints.LowerCenter]);
		es.AddWaypoint(wayPoints[Waypoints.SwordsmanTower]);
		es.AddWaypoint(wayPoints[Waypoints.Center]);
		es.AddWaypoint(wayPoints[Waypoints.King]);
		units.Add (es);
		
		es = new EnemySquad (7, wayPoints[Waypoints.Center]);
		es.AddWaypoint(wayPoints[Waypoints.MageTower]);;
		es.AddWaypoint(wayPoints[Waypoints.King]);
		units.Add (es);
		
		es = new EnemySquad (7, wayPoints[Waypoints.LowerCenter]);
		es.AddWaypoint(wayPoints[Waypoints.ArcherTower]);
		es.AddWaypoint(wayPoints[Waypoints.LowerCenter]);
		es.AddWaypoint(wayPoints[Waypoints.King]);
		units.Add (es);
	}
}