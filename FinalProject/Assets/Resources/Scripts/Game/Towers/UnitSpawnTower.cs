using UnityEngine;
using System.Collections;

public class UnitSpawnTower : TowerBehavior {

    // private SquadManager squads;

    void Start()
    {
        type = TOWERTYPE.Unit;
        health = 100;
    }

    public override void Click()
    {
        throw new System.NotImplementedException();
    }

}
