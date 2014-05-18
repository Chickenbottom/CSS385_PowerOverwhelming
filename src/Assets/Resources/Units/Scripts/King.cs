using UnityEngine;
using System.Collections;

public class King : Unit
{
    protected override void Awake ()
    {
        mUnitType = UnitType.King;
        
        mMeleeWeapon = new Sword ();
        mCurrentWeapon = mMeleeWeapon;
        
        base.Awake();
        
        GameState.KingsHealth = mMaxHealth;
    }
    
    public override void Damage (int damage)
    {
        base.Damage (damage);
        GameState.KingsHealth = mHealth;
    }
}
