using UnityEngine;
using System.Collections;

public class UnitSpawningTower : Tower {

    // private SquadManager squads;

    void Start()
    {
        type = TowerType.kUnitSpawner;
        health = 100;
    }

    public override void Click()
    {
        Debug.Log("YAY IT WORKED" + health + "  " + type);
    }

}
