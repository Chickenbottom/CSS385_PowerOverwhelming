using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swordsman : Unit
{
    protected override void Awake ()
    {
        mUnitType = UnitType.Swordsman;
                   
        mMeleeWeapon = new Sword ();
        mCurrentWeapon = mMeleeWeapon;
        
        base.Awake();
    }
}
