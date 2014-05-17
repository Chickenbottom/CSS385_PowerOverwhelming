using UnityEngine;
using System.Collections;

public class AbilityTower: Tower {

    public Ability ability;

    void Start()
    {
        towerType = TowerType.Ability;
        GameObject.Find("TargetFinder").GetComponent<TowerTargets>().AddTower(this);
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
