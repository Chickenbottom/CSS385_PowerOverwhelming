using UnityEngine;
using System.Collections;

public class Dagger : Weapon
{       
    public Dagger ()
    {      
        mWeaponType = WeaponType.Dagger;
        this.InitializeStats();
        
        reloadTimer = ReloadTime;
    }
}
