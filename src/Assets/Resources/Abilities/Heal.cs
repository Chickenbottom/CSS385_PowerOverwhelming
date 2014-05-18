using UnityEngine;
using System.Collections;

public class Heal : Ability
{
    private int mTarget = -1;

    public override void UseAbility (Vector3 mousePos)
    {
        // add healing effects
        Tower t = GameObject.Find ("TargetFinder").GetComponent<TowerTargets> ().GetTarget (mousePos);
        if (t.Allegiance == Allegiance.Rodelle)
            t.Damage (-1 * t.MaxHealth);
    }

    public override bool ValidMousePos (Vector3 mousePos)
    {
        mTarget = GameObject.Find ("TargetFinder").GetComponent<TowerTargets> ().ValidMousePos (mousePos);
        return mTarget > -1;
    }

}
