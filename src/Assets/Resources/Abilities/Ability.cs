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

    // Abilities won't work on the towers that they belong to (at least heal won't)
    public abstract bool ValidMousePos (Vector3 mousePos);

    public abstract void UseAbility (Vector3 mousePos);
}
