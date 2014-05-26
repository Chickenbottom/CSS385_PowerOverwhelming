using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour
{
    public float CoolDown {
        get { return mCooldown; } }
    public float CooldownTimer {
        get { return Time.time - mUseTimer; } }
    
    protected float mCooldown;
    protected float mUseTimer;

    public abstract void UseAbility (Target target);
}
