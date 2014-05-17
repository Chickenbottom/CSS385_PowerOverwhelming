using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Archer : Unit
{
    protected new void Awake ()
    {
        base.Awake ();
        
        mDefaultHealth = 4;
        
        mMovementSpeed = 8f; // units per second
        mChargeSpeed = 10f;   // speed used to engage enemies
        
        mMeleeWeapon = new Dagger ();
        mRangedWeapon = new Crossbow ();
        mCurrentWeapon = mRangedWeapon;
    }
    
    void Start ()
    {       
        mHealth = mDefaultHealth;
    }
}
