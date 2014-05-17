using UnityEngine;
using System.Collections;

public class Heal : Ability {

    int target = -1;

    public override void UseAbility(Vector3 mousePos)
    {
        // add healing effects
        GameObject.Find("TargetFinder").GetComponent<TowerTargets>().GetTarget(mousePos).health += 20;
    }

    public override bool ValidMousePos(Vector3 mousePos)
    {
        target = GameObject.Find("TargetFinder").GetComponent<TowerTargets>().ValidMousePos(mousePos);
        return target > -1;
    }

}
