using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mage : Unit
{   
    protected override void Awake ()
    {
        mUnitType = UnitType.Mage;
        mMeleeWeapon = new Dagger ();
        mRangedWeapon = new IceWand ();
        mCurrentWeapon = mRangedWeapon;
        
        ((IceWand)mRangedWeapon).src = this;
        
        base.Awake();
    }
}
