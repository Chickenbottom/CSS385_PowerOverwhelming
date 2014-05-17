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
        
        mWeapons = new SortedList ();
        
        Weapon crossbow = new Crossbow ();
        mCurrentWeapon = crossbow;
        mWeapons.Add (crossbow.Range, crossbow);
        
        Weapon dagger = new Dagger ();
        mWeapons.Add (dagger.Range, dagger);
    }
    
    void Start ()
    {       
        mHealth = mDefaultHealth;
    }
}
