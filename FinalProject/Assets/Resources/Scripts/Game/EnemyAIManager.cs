using UnityEngine;
using System.Collections;

public class EnemyAIManager : MonoBehaviour
{

    private GameObject mEnemy;
    private GameObject mTarget;
    private GameObject mBestDestroyedTower;

    private ArrayList mTargets;
    private ArrayList mDestroyedTowers;
    private ArrayList mCurEnemies;

    private float mEnemySpawnInterval;
    private float mLastEnemySpawn;
    private float mLastTargetChange;
    private float mTargetChangeInterval;

    void Start()
    {
        mCurEnemies = new ArrayList();
        mTargets = new ArrayList();
        mDestroyedTowers = new ArrayList();
        mBestDestroyedTower = null;
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
        GameObject newEnemy = (GameObject) Instantiate(mEnemy, new Vector3(0f, -210f, 0f), new Quaternion());
        mCurEnemies.Add(newEnemy);
        //newEnemy.SetDestination(new Vector3(0f, -180f, 0f));
        //newEnemy.UpdateTarget(mTarget);
    }

    public GameObject getTarget()
    {
        return mTarget;
    }

    public void TowerDestroyed(GameObject tower)
    {
        mTargets.Remove(tower);
        mDestroyedTowers.Add(tower);
        GetBestTower();
        //foreach (Squad s in mCurEnemies)
        //{
        //    s.GetWeapons();
        //}
    }

    private void GetBestTower()
    {

    }

    public void TowerRepaired(GameObject tower)
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
