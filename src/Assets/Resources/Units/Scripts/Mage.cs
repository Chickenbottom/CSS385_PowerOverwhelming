using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mage : Unit
{
    protected new void Awake ()
    {
        base.Awake ();
        
        mDefaultHealth = 4;
        
        mMovementSpeed = 8f; // units per second
        mChargeSpeed = 10f;  // speed used to engage enemies
        
        mMeleeWeapon = new Dagger ();
        mRangedWeapon = new IceWand ();
        mCurrentWeapon = mRangedWeapon;
        
        ((IceWand)mRangedWeapon).src = this;
    }
    
    void Start ()
    {       
        mHealth = mDefaultHealth;
    }
}
