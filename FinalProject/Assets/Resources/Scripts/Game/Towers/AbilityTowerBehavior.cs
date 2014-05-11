using UnityEngine;
using System.Collections;

public class AbilityTowerBehavior: TowerBehavior {

    public override void Click()
    {
        Debug.Log("YAY IT WORKED" + health + "  " + type);
    }

    void Start()
    {
        type = TOWERTYPE.Ability;
        health = 100;
    }

}
