using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swordsman : Unit
{
    protected new void Awake ()
    {
        base.Awake ();
        mDefaultHealth = 4;
        
        mMovementSpeed = 10f; // units per second
        mChargeSpeed = 12f;   // speed used to engage enemies
           
        mMeleeWeapon = new Sword ();
        mCurrentWeapon = mMeleeWeapon;
    }

    void Start ()
    {           
        mHealth = mDefaultHealth;
    }
}
