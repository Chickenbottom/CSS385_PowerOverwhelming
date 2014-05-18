using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Archer : Unit
{
    protected override void Awake ()
    {
        mUnitType = UnitType.Archer;
        mMeleeWeapon = new Dagger ();
        mRangedWeapon = new Crossbow ();
        mCurrentWeapon = mRangedWeapon;
        
        base.Awake();
    }
}
