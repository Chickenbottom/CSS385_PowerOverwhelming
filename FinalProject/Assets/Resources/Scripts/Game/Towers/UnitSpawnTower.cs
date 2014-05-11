using UnityEngine;
using System.Collections;

public class UnitSpawnTower : TowerBehavior {

    // private SquadManager squads;

    public override void Click()
    {
        throw new System.NotImplementedException();
    }

    public override TOWERTYPE GetTowerType()
    {
        return TOWERTYPE.Unit;
    }

}
