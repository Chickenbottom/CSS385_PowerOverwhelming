using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIManager : MonoBehaviour
{

	private enum Waypoints {
		kKing,
		kArcherTower,
		kAbilityTower,
		kSwordsmanTower,
		kMageTower,
		kCenter,
		kLowerCenter,
		kSpawnPoint,
	}
	Dictionary<Waypoints, Vector3> mWaypoints;
	
	
	public int NumWaves = 3;
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	///////////////////////////////////////////////////////////////////////////////////	
	
	void Update()
	{
		if (mWaveSpawnInterval < Time.time - mLastEnemySpawn) {
			mLastEnemySpawn = Time.time;
			this.SpawnWave();
		}
		
		for (int i = mUnits.Count - 1; i >= 0; --i) {
			if (mUnits[i].IsDead)
				mUnits.RemoveAt(i);
			else 
				mUnits[i].Update();
			
		}
		
		if (mCurrentWave >= NumWaves && mUnits.Count == 0) {
			//GameState.TriggerWin();
		}
		
		if (Input.GetButtonDown("Fire1")) {
			SpawnWave3 ();
		}
	}
	
	void Start()
	{
		mLastEnemySpawn = Time.time;
		mUnits = new List<EnemySquad> ();
		
		mWaypoints = new Dictionary<Waypoints, Vector3>();
		mWaypoints.Add (Waypoints.kAbilityTower, new Vector3(46, -40, 0));
		mWaypoints.Add (Waypoints.kArcherTower, new Vector3(-105, -40, 0));
		mWaypoints.Add (Waypoints.kMageTower, new Vector3(45, 50, 0));
		mWaypoints.Add (Waypoints.kSwordsmanTower, new Vector3(-108, 50, 0));
		mWaypoints.Add (Waypoints.kKing, new Vector3(-33, 56, 0));
		mWaypoints.Add (Waypoints.kCenter, new Vector3(-33, 10, 0));
		mWaypoints.Add (Waypoints.kLowerCenter, new Vector3(-33, -37, 0));
		mWaypoints.Add (Waypoints.kSpawnPoint, new Vector3(-33, -64, 0));
		
		SpawnWave();
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
	
	private List<EnemySquad> mUnits;
	
	private void SpawnWave()
	{
		mCurrentWave ++;
		switch (mCurrentWave)
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
		es = new EnemySquad (4, mWaypoints[Waypoints.kLowerCenter]);
		es.AddWaypoint(mWaypoints[Waypoints.kKing]);
		mUnits.Add (es);
	}
	
	void SpawnWave2()
	{
		EnemySquad es = null;
		es = new EnemySquad (5, mWaypoints[Waypoints.kLowerCenter]);
		es.AddWaypoint(mWaypoints[Waypoints.kArcherTower]);
		es.AddWaypoint(mWaypoints[Waypoints.kKing]);
		mUnits.Add (es);
		
		es = new EnemySquad (5, mWaypoints[Waypoints.kCenter]);
		es.AddWaypoint(mWaypoints[Waypoints.kMageTower]);
		es.AddWaypoint(mWaypoints[Waypoints.kCenter]);
		mUnits.Add (es);
	}
	
	void SpawnWave3()
	{
		EnemySquad es = null;
		es = new EnemySquad (7, mWaypoints[Waypoints.kLowerCenter]);
		es.AddWaypoint(mWaypoints[Waypoints.kSwordsmanTower]);
		es.AddWaypoint(mWaypoints[Waypoints.kCenter]);
		es.AddWaypoint(mWaypoints[Waypoints.kKing]);
		mUnits.Add (es);
		
		es = new EnemySquad (7, mWaypoints[Waypoints.kCenter]);
		es.AddWaypoint(mWaypoints[Waypoints.kMageTower]);
		es.AddWaypoint(mWaypoints[Waypoints.kCenter]);
		es.AddWaypoint(mWaypoints[Waypoints.kKing]);
		mUnits.Add (es);
		
		es = new EnemySquad (7, mWaypoints[Waypoints.kLowerCenter]);
		es.AddWaypoint(mWaypoints[Waypoints.kArcherTower]);
		es.AddWaypoint(mWaypoints[Waypoints.kLowerCenter]);
		es.AddWaypoint(mWaypoints[Waypoints.kKing]);
		mUnits.Add (es);
	}
}