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
        GameObject.Find ("TargetFinder").GetComponent<TowerTargets> ().AddTower (this);
    }

    public override void SetTarget (Vector3 location)
    {
        ability.UseAbility (location);
        CooldownBar.UpdateValue((int)(ability.CooldownTimer * 100));
    }

    public override bool ValidMousePos (Vector3 mousePos)
    {
        return ability.ValidMousePos (mousePos);
    }

    void OnGUI()
    {
        CooldownBar.UpdateValue((int)(ability.CooldownTimer * 100));
    }

}
