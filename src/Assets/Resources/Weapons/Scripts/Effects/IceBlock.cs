using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceBlock : MonoBehaviour
{
    public void Freeze (Unit unit)
    {         
        mUnit = unit;
        
        if (mFrozenEffectPrefab == null)
            mFrozenEffectPrefab = Resources.Load ("Weapons/IceBlockPrefab") as GameObject;
        
        mStartTimer = Time.time;
        
        mIceBlock = (GameObject) Instantiate (mFrozenEffectPrefab);
        
        if (unit is Elite)
            unit.BuffMovement (0.5f, mDuration);
        else
            unit.BuffMovement(0f, mDuration);
        
        FollowUnit ();
        mIsInitialized = true;
    }
    
    private bool mIsInitialized = false;
    private float mDuration = 3f;
    private float timer;
    private float mStartTimer;
    
    private Target mUnit;
    private GameObject mIceBlock;
    
    private static GameObject mFrozenEffectPrefab;
        
    void Update ()
    {
        if (! mIsInitialized)
            return;
            
        if (Time.time - mStartTimer > mDuration || mUnit == null || mUnit.IsDead) {
            Unfreeze ();
            return;
        }
        
        FollowUnit ();
    }
    
    public void Unfreeze ()
    {
        Destroy (mIceBlock.gameObject);
        Destroy (this);
        mIsInitialized = false;
    }
    
    private void FollowUnit()
    {
        mIceBlock.transform.position = mUnit.Position + new Vector3(0f, -5f, 0f);
        Utility.Perspectivize (mIceBlock);
    }
}
