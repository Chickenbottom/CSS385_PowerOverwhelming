using UnityEngine;
using System.Collections;

public class TowerBomb : Ability
{
    protected int mDamage = 1000;
    protected static GameObject mTowerBombPrefab;
    
    void Awake()
    {
        mCooldown = 12f;
        mUseTimer = -mCooldown;
    }
    
    // does nothing
    public override void UseAbility (Target target) {}
    
    public override void UsePositionalAbility (Vector3 location)
    {
        if (Time.time - mUseTimer > mCooldown) {
            GameObject o = (GameObject)GameObject.Instantiate (mTowerBombPrefab);
            TowerBombEffect b = (TowerBombEffect)o.GetComponent<TowerBombEffect>();
            b.ExplodeAt (location, mDamage);
            mUseTimer = Time.time;
        }
    }
    
    void Start()
    {
        if (mTowerBombPrefab == null)
            mTowerBombPrefab = Resources.Load("Abilities/TowerBombPrefab") as GameObject;
    }
}
