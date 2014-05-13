using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIManager : MonoBehaviour
{
	private GameObject mEnemy;
	private GameObject mTarget;
	private GameObject mBestDestroyedTower;
	
	private ArrayList mTargets;
	private ArrayList mDestroyedTowers;
	private ArrayList mCurEnemies;
	
	private float mWaveSpawnInterval = 3.0f;
	private float mLastEnemySpawn;
	private float mLastTargetChange;
	private float mTargetChangeInterval;

	struct EnemySquad {
		public Squad squad;
		public int currentWaypoint;
		public List<Vector3> waypoints;

		public EnemySquad(Squad s, List<Vector3> inWaypoints) {
			squad = s;
			waypoints = inWaypoints;
			s.UpdateSquadDestination(waypoints[0]);
			currentWaypoint = 0;
		}
	}

	List<EnemySquad> mUnits;

	void Start()
	{
		mLastEnemySpawn = Time.time;
		mUnits = new List<EnemySquad> ();

	}
	
	static GameObject mSquadPrefab;
	private void SpawnWave(int wave)
	{
		if (mSquadPrefab == null) 
			mSquadPrefab = Resources.Load ("Squads/SquadPrefab") as GameObject;

		Debug.Log ("Wave: " + wave);

		GameObject o = (GameObject) GameObject.Instantiate (mSquadPrefab);
		Squad s = o.GetComponent<Squad> ();

		s.NumSquadMembers = 4;
		s.Spawn (new Vector3(0f, -100f, 0f), UnitType.kPeasant);
		List<Vector3> waypoints = new List<Vector3> ();
		waypoints.Add(new Vector3 (0f, 50f, 0f));

		EnemySquad es = new EnemySquad (s, waypoints);
		mUnits.Add (es);
	}

	void Update()
	{
		if (mWaveSpawnInterval < Time.time - mLastEnemySpawn) {
			mLastEnemySpawn = Time.time;
			this.SpawnWave(0);
		}
	}

	public void TowerDestroyed(GameObject tower)
	{
		mTargets.Remove(tower);
		mDestroyedTowers.Add(tower);
	}

	public void AddTarget(GameObject target)
	{
		mTargets.Add(target);
	}
	
	private void ChangeTarget()
	{
		int lastTarget = mTargets.IndexOf(mTarget);
		int nextTarget = Random.Range(0, mTargets.Count);
		while (nextTarget == lastTarget)
		{
			nextTarget = Random.Range(0, mTargets.Count);
		}
		mTarget = (GameObject) mTargets[nextTarget];
		//foreach (Squad s in mCurEnemies)
		//{
		//    s.UpdateTarget(mTarget);
		//}
	}
	
}
