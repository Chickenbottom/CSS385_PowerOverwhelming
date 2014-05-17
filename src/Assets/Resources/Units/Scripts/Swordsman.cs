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
        mChargeSpeed = 15f;   // speed used to engage enemies
        
        mWeapons = new SortedList ();
        
        Weapon sword = new Sword ();
        mCurrentWeapon = sword;
        mWeapons.Add (sword.Range, sword);
    }

    void Start ()
    {           
        mHealth = mDefaultHealth;
    }
}
