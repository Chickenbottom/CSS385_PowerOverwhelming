using UnityEngine;
using System.Collections;

public class AbilityTowerBehavior: TowerBehavior {

    public override void Click()
    {
        throw new System.NotImplementedException();
    }

    public override TOWERTYPE GetTowerType()
    {
        return TOWERTYPE.Ability;
    }

}
