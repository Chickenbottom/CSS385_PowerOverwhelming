﻿using UnityEngine;
using System.Collections;

public class AbilityTower: Tower {

    public override void Click()
    {
        Debug.Log("YAY IT WORKED" + health + "  " + type);
    }

    void Start()
    {
        type = TowerType.kAbility;
        health = 100;
    }

	public override void SetTarget(Vector3 location)
	{
		// do nothing
	}
}
