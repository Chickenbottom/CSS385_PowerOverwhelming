using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Waypoint {
    King,
    ArcherTower,
    AbilityTower,
    SwordsmanTower,
    MageTower,
    Center,
    LowerCenter,
    SpawnPoint,
}

public class EnemyAI : MonoBehaviour
{
	public Dictionary<Waypoint, Vector3> Waypoints;
	public int NumWaves = 3;
	
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
		
		if (currentWave >= NumWaves && units.Count == 0) {
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
		
		Waypoints = new Dictionary<Waypoint, Vector3>();
		Waypoints.Add (Waypoint.AbilityTower, new Vector3(43, -40, 0));
		Waypoints.Add (Waypoint.ArcherTower, new Vector3(-105, -40, 0));
		Waypoints.Add (Waypoint.MageTower, new Vector3(43, 50, 0));
		Waypoints.Add (Waypoint.SwordsmanTower, new Vector3(-108, 50, 0));
		Waypoints.Add (Waypoint.King, new Vector3(-33, 56, 0));
		Waypoints.Add (Waypoint.Center, new Vector3(-33, 10, 0));
		Waypoints.Add (Waypoint.LowerCenter, new Vector3(-33, -37, 0));
		Waypoints.Add (Waypoint.SpawnPoint, new Vector3(-33, -64, 0));
		
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
			//SpawnWave2();
			break;
		case(3):
			//SpawnWave3();
			break;
		}
	}
	
	// TODO fix this hard coding of enemy waves
	void SpawnWave1()
	{
		EnemySquad es = null;
        es = new EnemySquad (4, Waypoints[Waypoint.King], 1f);
		units.Add (es);
		
        es = new EnemySquad (5, Waypoints[Waypoint.King], 5f);
        units.Add (es);
        
        es = new EnemySquad (6, Waypoints[Waypoint.King], 7f);
        units.Add (es);
        
		es = new EnemySquad (7, Waypoints[Waypoint.LowerCenter], 0f);
		es.AddWaypoint(Waypoints[Waypoint.ArcherTower]);
		es.AddWaypoint(Waypoints[Waypoint.King]);
        units.Add (es);
        
        es = new EnemySquad (7, Waypoints[Waypoint.LowerCenter], 3f);
        es.AddWaypoint(Waypoints[Waypoint.AbilityTower]);
        es.AddWaypoint(Waypoints[Waypoint.King]);
        units.Add (es);
        
        es = new EnemySquad (7, Waypoints[Waypoint.LowerCenter], 6f);
        es.AddWaypoint(Waypoints[Waypoint.ArcherTower]);
        es.AddWaypoint(Waypoints[Waypoint.King]);
		units.Add (es);
	}
	
	void SpawnWave2()
	{
		EnemySquad es = null;
		es = new EnemySquad (5, Waypoints[Waypoint.LowerCenter]);
		es.AddWaypoint(Waypoints[Waypoint.ArcherTower]);
		es.AddWaypoint(Waypoints[Waypoint.King]);
		units.Add (es);
		
		es = new EnemySquad (5, Waypoints[Waypoint.Center]);
		es.AddWaypoint(Waypoints[Waypoint.MageTower]);
		es.AddWaypoint(Waypoints[Waypoint.King]);
		units.Add (es);
	}
	
	void SpawnWave3()
	{
		EnemySquad es = null;
		es = new EnemySquad (7, Waypoints[Waypoint.LowerCenter]);
		es.AddWaypoint(Waypoints[Waypoint.SwordsmanTower]);
		es.AddWaypoint(Waypoints[Waypoint.Center]);
		es.AddWaypoint(Waypoints[Waypoint.King]);
		units.Add (es);
		
		es = new EnemySquad (7, Waypoints[Waypoint.Center]);
		es.AddWaypoint(Waypoints[Waypoint.MageTower]);;
		es.AddWaypoint(Waypoints[Waypoint.King]);
		units.Add (es);
		
		es = new EnemySquad (7, Waypoints[Waypoint.LowerCenter]);
		es.AddWaypoint(Waypoints[Waypoint.ArcherTower]);
		es.AddWaypoint(Waypoints[Waypoint.LowerCenter]);
		es.AddWaypoint(Waypoints[Waypoint.King]);
		units.Add (es);
	}
}