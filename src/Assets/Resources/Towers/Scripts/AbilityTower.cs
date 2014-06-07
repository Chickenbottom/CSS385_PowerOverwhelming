using UnityEngine;
using System.Collections;

public class AbilityTower: Tower
{
    public Ability ability;
    public SpriteRenderer Glow;

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
    
    void Update()
    {
        if (Glow == null)
            return;
        
        Glow.enabled = (ability.CountdownTimer <= 0);
    }
}
