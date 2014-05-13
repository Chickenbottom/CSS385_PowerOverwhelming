using UnityEngine;
using System.Collections;

public class AbilityTower: Tower {

    public override void Click()
    {
    }

    void Start()
    {
        mTowerType = TowerType.kAbility;
        mHealth = 100;
    }

	public override void SetTarget(Vector3 location)
	{
		// do nothing
	}
}
