using UnityEngine;
using System.Collections;

public class Peasant : Unit
{
    protected new void Awake ()
    {
        base.Awake ();
    
        SightRange = 10f;
        mDefaultHealth = 5;
        
        mMovementSpeed = 12f; // units per second
        mChargeSpeed = 14f;   // speed used to engage enemies
        
        mMeleeWeapon = new Pitchfork ();
        mCurrentWeapon = mMeleeWeapon;
    }
    
    void Start ()
    {       
        mHealth = mDefaultHealth;
    }
}
