using UnityEngine;
using System.Collections;

public class AbilityTower: Tower
{
    public Ability ability;
    public Progressbar CooldownBar;
    
    void Start ()
    {
        towerType = TowerType.Ability;
        CooldownBar.MaxValue = (int)(ability.CoolDown * 100);
    }

    public override void UseTargetedAbility (Target target)
    {
        if (target == this) 
            return;
            
        ability.UseAbility (target);
        CooldownBar.UpdateValue((int)(ability.CooldownTimer * 100));
    }

    public override void SetDestination (Vector3 destination)
    {
        GameObject.Find ("Towers").GetComponent<MouseManager>().Deselect(this);
    }

    void OnGUI()
    {
        CooldownBar.UpdateValue((int)(ability.CooldownTimer * 100));
    }
}
