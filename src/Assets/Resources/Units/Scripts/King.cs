using UnityEngine;
using System.Collections;

public class King : Unit
{
    protected override void Awake ()
    {
        mUnitType = UnitType.King;
        base.Awake();
        this.InitializeStats();
        
        mMeleeWeapon = new Sword (mLevel);
        mCurrentWeapon = mMeleeWeapon;
        
        GameState.KingsHealth = mMaxHealth;
    }
    
    public override void Damage (int damage)
    {
        base.Damage (damage);
        GameState.KingsHealth = mHealth;
    }
}
