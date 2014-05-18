using UnityEngine;
using System.Collections;

public class Heal : Ability {

    int target = -1;

    public override void UseAbility(Vector3 mousePos)
    {
        // add healing effects
        Tower t = GameObject.Find("TargetFinder").GetComponent<TowerTargets>().GetTarget(mousePos);
        if (t.Allegiance == Allegiance.Rodelle)
            t.Damage(-1 * t.MaxHealth);
    }

    public override bool ValidMousePos(Vector3 mousePos)
    {
        target = GameObject.Find("TargetFinder").GetComponent<TowerTargets>().ValidMousePos(mousePos);
        return target > -1;
    }

}
