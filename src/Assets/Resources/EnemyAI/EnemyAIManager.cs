using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIManager : MonoBehaviour
{
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	///////////////////////////////////////////////////////////////////////////////////	
	
	void Update()
	{
		if (mWaveSpawnInterval < Time.time - mLastEnemySpawn) {
			mLastEnemySpawn = Time.time;
			this.SpawnWave();
		}
		
		foreach (EnemySquad e in mUnits) {
			e.Update();
		}
	}
	
	void Start()
	{
		mLastEnemySpawn = Time.time;
		mUnits = new List<EnemySquad> ();
	}

	///////////////////////////////////////////////////////////////////////////////////
	// Public Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////	
	
	
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////	
	private float mWaveSpawnInterval = 2.0f;
	private float mLastEnemySpawn;
	private int mCurrentWave = 1;
	
	private List<EnemySquad> mUnits;
	
	private void SpawnWave()
	{
		switch (mCurrentWave)
		{
		case(1):
			SpawnWave2();
			break;
		case(2):
			//SpawnWave1();
			break;
		case(3):
			//SpawnWave3 ();
			break;
		}
		
		mCurrentWave ++;
	}
	
	
	// TODO fix this hard coding of enemy waves
	void SpawnWave1()
	{
		EnemySquad es = null;
		es = new EnemySquad (7, new Vector3 (0f, -55f, 0f));
		es.AddWaypoint(new Vector3(50f, -40f, 0f));
		es.AddWaypoint(new Vector3(-50f, 60f, 0f));
		mUnits.Add (es);
	}
	
	void SpawnWave2()
	{
		EnemySquad es = null;
		es = new EnemySquad (7, new Vector3 (-85f, -55f, 0f));
		es.AddWaypoint(new Vector3 (0f, -40f, 0f));
		es.AddWaypoint(new Vector3 (0f, 75f, 0f));
		mUnits.Add (es);
	}
	
	void SpawnWave3()
	{
		EnemySquad es = null;
		es = new EnemySquad (1, new Vector3 (-50f, 0f, 0f));
		mUnits.Add (es);
	}
}