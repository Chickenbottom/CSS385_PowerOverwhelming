using UnityEngine;
using System.Collections;

public class AbilityTower: Tower {

    public override void Click()
    {
    }

    void Start()
    {
        towerType = TowerType.Ability;
        health = 100;
    }

	public override void SetTarget(Vector3 location)
	{
		// do nothing
	}
}
