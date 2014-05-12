using UnityEngine;
using System.Collections;

public class EnemyAIManager : MonoBehaviour
{

    private GameObject mEnemy;
    private GameObject mTarget;

    private ArrayList mTargets;
    private ArrayList mCurEnemies;

    private float mEnemySpawnInterval;
    private float mLastEnemySpawn;
    private float mLastTargetChange;
    private float mTargetChangeInterval;

    void Start()
    {
        mCurEnemies = new ArrayList();
        mTargets = new ArrayList();
        mEnemySpawnInterval = 20f;
        mLastEnemySpawn = Time.time;
        mTarget = null;
        mEnemy = null;
        mLastTargetChange = Time.time;
        mTargetChangeInterval = 45f;
    }

    void Update()
    {
        if (mEnemySpawnInterval < Time.time - mLastEnemySpawn)
        {
            mLastEnemySpawn = Time.time;
            Spawn();
        }
        if (mTargetChangeInterval < Time.time - mLastTargetChange)
        {
            ChangeTarget();
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

    public void TowerDestroyed(GameObject target)
    {

    }

    public void TowerRepaired(GameObject target)
    {

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
