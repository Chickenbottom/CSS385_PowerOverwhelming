using UnityEngine;
using System.Collections;

public class King : Unit
{
    protected new void Awake ()
    {
        base.Awake ();
        
        SightRange = 0f;
        mDefaultHealth = 100;
        
        mMovementSpeed = 0f; // units per second
        mChargeSpeed = 0f;   // speed used to engage enemies
        
        GameState.KingsHealth = mDefaultHealth;
        
        mMeleeWeapon = new Sword ();
        mCurrentWeapon = mMeleeWeapon;
    }
    
    public void Start ()
    {
        mHealth = mDefaultHealth;
    }
    
    public override void Damage (int damage)
    {
        base.Damage (damage);
        GameState.KingsHealth = mHealth;
    }
}
