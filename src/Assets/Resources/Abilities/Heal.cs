using UnityEngine;
using System.Collections;

public class Heal : Ability {

    public override void UseAbility(Vector3 mousePos)
    {
        // add healing effects
        GameObject.Find("TargetFinder").GetComponent<TowerTargets>().GetTarget(mousePos);
    }

    public override bool ValidMousePos(Vector3 mousePos)
    {
        int target = GameObject.Find("TargetFinder").GetComponent<TowerTargets>().ValidMousePos(mousePos);
        return target > -1;
    }

}
