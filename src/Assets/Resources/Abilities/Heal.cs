using UnityEngine;
using System.Collections;

public class Heal : Ability {

    private int target;

    public override void UseAbility(Vector3 MousePos)
    {
        // add healing
        GameObject.Find("MouseFinder").GetComponent<TowerTargets>().GetTarget(target);
    }

    public override bool ValidMousePos(Vector3 mousePos)
    {
        target = GameObject.Find("MouseFinder").GetComponent<TowerTargets>().ValidMousePos(mousePos);
        return target > -1;
    }

}
