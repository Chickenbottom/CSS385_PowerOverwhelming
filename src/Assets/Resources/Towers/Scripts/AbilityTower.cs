using UnityEngine;
using System.Collections;

public class AbilityTower: Tower 
{
    public Ability ability;

    void Start()
    {
        towerType = TowerType.Ability;
        health = 100;
    }

	// when right-clicking on a tower or the king
	public override void UseTargetedAbility(Target target) 
	{
		ability.UseTargetedAbility(target);
	}
	
	// when right-clicking on the game area
	public override void UsePositionalAbility(Vector3 position) 
	{
		// TODO can add a check for the ClickBox here
		ability.UsePositionAbility(position);
	} 

	// does nothing on left-click?
	public override void SetTarget(Vector3 location)
	{
        //ability.UseAbility(location);
	}
}
