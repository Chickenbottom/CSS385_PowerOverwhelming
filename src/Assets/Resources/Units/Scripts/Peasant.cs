using UnityEngine;
using System.Collections;

public class Peasant : Unit
{
    protected override void Awake ()
    {
        mUnitType = UnitType.Peasant;
        
        mMeleeWeapon = new Pitchfork ();
        mCurrentWeapon = mMeleeWeapon;
        
        base.Awake();
    }
}
