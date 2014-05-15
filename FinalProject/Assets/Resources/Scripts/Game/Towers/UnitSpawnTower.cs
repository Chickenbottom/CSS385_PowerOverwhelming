using UnityEngine;
using System.Collections;

public class UnitSpawnTower : TowerBehavior {

    // private SquadManager squads;

    void Start()
    {
        type = TOWERTYPE.Unit;
        health = 100;
        SharedStart();
    }

    public override void Click()
    {
        Debug.Log("YAY IT WORKED" + health + "  " + type);
    }

}
