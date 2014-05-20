using UnityEngine;
using System.Collections;

public class Heal : Ability
{
    private int mTarget = -1;
    private bool mReadyToUse = true;
    private float mCoolDown = 15f;

    public override void UseAbility (Vector3 mousePos)
    {
        // add healing effects
        if (mReadyToUse)
        {
            Tower t = GameObject.Find("TargetFinder").GetComponent<TowerTargets>().GetTarget(mousePos);
            if (t.Allegiance == Allegiance.Rodelle)
            {
                t.Damage(-1 * t.MaxHealth);
                mReadyToUse = false;
                // Add method to activate cooldown bar
                Invoke("SetReadyToUse", mCoolDown);
            }
        }
    }

    public override bool ValidMousePos (Vector3 mousePos)
    {
        mTarget = GameObject.Find ("TargetFinder").GetComponent<TowerTargets> ().ValidMousePos (mousePos);
        return mTarget > -1;
    }

    private void SetReadyToUse()
    {
        mReadyToUse = true;
    }

}
