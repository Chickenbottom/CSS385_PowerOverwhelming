using UnityEngine;
using System.Collections;

public class AbilityTower: Tower
{
    public Ability ability;
    
    void Start ()
    {
        towerType = TowerType.Ability;
    }

    public override void UseTargetedAbility (Target target)
    {
        if (target == this) 
            return;
            
        ability.UseAbility (target);
    }

    public override void SetDestination (Vector3 destination)
    {
        GameObject.Find ("Towers").GetComponent<MouseManager>().Deselect(this);
    }
}
