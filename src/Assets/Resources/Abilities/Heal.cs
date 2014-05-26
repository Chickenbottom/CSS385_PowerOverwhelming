using UnityEngine;
using System.Collections;

public class Heal : Ability
{
    protected float mHealRate = 0.5f;

    void Awake()
    {
        mCooldown = 10f;
        mUseTimer = -mCooldown;
    }

    public override void UseAbility (Target target)
    {
        if (Time.time - mUseTimer > mCooldown) {
            target.Damage((int)(-mHealRate * target.MaxHealth));
            mUseTimer = Time.time;
        }
    }
}
