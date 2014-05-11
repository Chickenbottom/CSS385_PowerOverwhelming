using UnityEngine;
using System.Collections;

public class AbilityTowerBehavior: TowerBehavior {

    public override void Click()
    {
        Debug.Log("YAY IT WORKED");
    }

    public override TOWERTYPE GetTowerType()
    {
        return TOWERTYPE.Ability;
    }

}
