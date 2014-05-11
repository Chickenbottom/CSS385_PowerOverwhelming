using UnityEngine;
using System.Collections;

public class EnemyAIManager : MonoBehaviour
{

    private ArrayList mCurEnemies;

    private GameObject mEnemy;
    private float mEnemySpawnInterval;
    private float mLastEnemySpawn;
    private GameObject mTarget;

    void Start()
    {
        mCurEnemies = new ArrayList();
        mEnemySpawnInterval = 20f;
        mLastEnemySpawn = Time.time;
        mTarget = null;
        mEnemy = null;
    }

    void Update()
    {
        if (mEnemySpawnInterval < Time.time - mLastEnemySpawn)
        {
            mLastEnemySpawn = Time.time;
            Spawn();
        }
    }

    private void Spawn()
    {
        mCurEnemies.Add(Instantiate(mEnemy, new Vector3(0f, -210f, 0f), new Quaternion()));
    }

    public GameObject getTarget()
    {
        return mTarget;
    }

    public void TowerDestroyed(GameObject tower)
    {

    }

}
