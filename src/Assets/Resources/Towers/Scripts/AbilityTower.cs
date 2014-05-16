using UnityEngine;
using System.Collections;

public class AbilityTower: Tower {

    public Ability ability;

    void Start()
    {
        towerType = TowerType.Ability;
        health = 100;
    }

	public override void SetTarget(Vector3 location)
	{
        ability.UseAbility(location);
	}

    public override bool ValidMousePos(Vector3 mousePos)
    {
        return ability.ValidMousePos(mousePos);
    }

}
