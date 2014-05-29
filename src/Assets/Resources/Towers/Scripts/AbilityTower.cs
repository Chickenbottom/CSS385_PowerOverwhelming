using UnityEngine;
using System.Collections;

public class AbilityTower: Tower
{
    public Ability ability;
    public Progressbar CooldownBar;

    public override void UseTargetedAbility (Target target)
    {
        if (target == this) 
            return;
            
        ability.UseAbility (target);
    }

    public override void SetDestination (Vector3 destination)
    {
        ability.UsePositionalAbility(destination);
    }
    
    protected virtual void Update()
    {
        CooldownBar.UpdateValue((int)(ability.CooldownTimer));
    }
    
    protected virtual void Start ()
    {
        base.Awake ();
        CooldownBar.MaxValue = (int)(ability.CoolDown);
        CooldownBar.UpdateValue((int)(ability.CooldownTimer));
    }
    
}
