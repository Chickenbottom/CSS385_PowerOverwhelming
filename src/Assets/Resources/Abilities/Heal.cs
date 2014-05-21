using UnityEngine;
using System.Collections;

public class Heal : Ability
{
    protected int mTarget = -1;
    protected float mHealRate = 0.25f;

    void Awake()
    {
        mCooldown = 15f;
        mUseTimer = -mCooldown;
    }

    public override void UseAbility (Vector3 mousePos)
    {
        if (Time.time - mUseTimer > mCooldown)
        {
            Tower t = GameObject.Find("TargetFinder").GetComponent<TowerTargets>().GetTarget(mousePos);
            if (t.Allegiance == Allegiance.Rodelle)
            {
                Debug.Log("Using heal on " + t);
                mUseTimer = Time.time;
                t.Damage((int)(-mHealRate * t.MaxHealth));
            }
        } else {
            //Debug.Log ((mCooldown - (Time.time - mUseTimer)).ToString() + " seconds remaining on heal cooldown.");
        }
    }

    public override bool ValidMousePos (Vector3 mousePos)
    {
        mTarget = GameObject.Find ("TargetFinder").GetComponent<TowerTargets> ().ValidMousePos (mousePos);
        return mTarget > -1;
    }
}
