using UnityEngine;
using System.Collections;

public class IceWand : Weapon
{
    private static GameObject projectilePrefab = null;
    
    public IceWand ()
    {       
        if (projectilePrefab == null)
            projectilePrefab = Resources.Load ("Weapons/IceBombPrefab") as GameObject;
            
        mWeaponType = WeaponType.IceWand;
        this.InitializeStats();
        reloadTimer = ReloadTime;
    }
    
    public Target src;

    protected override void PerformAttack (Target src, Target target)
    {
        if (target == null)
            return;
        //base.PerformAttack(src, target);
        reloadTimer = Random.Range (ReloadTime * (1f - ReloadVariance), ReloadTime * (1f + ReloadVariance));
        
        GameObject o = (GameObject)GameObject.Instantiate (projectilePrefab);
        IceBomb b = (IceBomb)o.GetComponent (typeof(IceBomb));
        b.transform.position = src.transform.position;
        b.SetParameters (src, target);
    }
}
