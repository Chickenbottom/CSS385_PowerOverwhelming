using UnityEngine;
using System.Collections;

public class EnemyAIManager : MonoBehaviour {

    private GameObject mEnemy;
    private float mEnemySpawnInterval;
    private float mLastEnemySpawn;
    private GameObject mTarget;

	void Start () {
        mEnemySpawnInterval = 20f;
        mLastEnemySpawn = Time.time;
        mTarget = null;
	}

	void Update () {
        if (mEnemySpawnInterval < Time.time - mLastEnemySpawn)
        {
            Spawn();
        }
	}

    private void Spawn()
    {
        // Spawn Enemies
    }

    public GameObject getTarget()
    {
        return mTarget;
    }

}
