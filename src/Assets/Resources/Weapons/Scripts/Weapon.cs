using UnityEngine;
using System.Collections;

public enum WeaponType
{
    Crossbow,
    Dagger,
    IceWand,
    Sword,
    Pitchfork,
}

public abstract class Weapon
{
    public int Damage { get; set; }

    public float Range { get; set; }

    public float ReloadTime { get; set; }
    
    public float ReloadVariance { get; set; }

    public float Accuracy { get; set; }
    
    protected float reloadTimer;
    protected int mLevel;
    
    public Weapon(int level)
    {
        mLevel = level;
    }
    
    public virtual void Attack (Target src, Target target)
    {
        if (target == null || src == null)
            return;
        
        reloadTimer -= Time.deltaTime;
        if (reloadTimer < 0) {
            PerformAttack (src, target);
        }
    }
    
    public virtual void Reset ()
    {
        reloadTimer = ReloadTime * 0.25f;
        //reloadTimer = Random.Range (ReloadTime * (1f - ReloadVariance), ReloadTime * (1f + ReloadVariance));
    }
    
    protected virtual void PerformAttack (Target src, Target target)
    {
        if (target == null)
            return;
        
        reloadTimer = Random.Range (ReloadTime * (1f - ReloadVariance), ReloadTime * (1f + ReloadVariance));
        
        if (Random.value < this.Accuracy)
            target.Damage (this.Damage);
    }
    
    protected WeaponType mWeaponType;
    
    protected void InitializeStats()
    {
        this.Damage = (int)WeaponUpgrades.GetStat(mWeaponType, WeaponStat.Damage);
        this.Range = WeaponUpgrades.GetStat(mWeaponType, WeaponStat.Range);
        this.ReloadTime = WeaponUpgrades.GetStat(mWeaponType, WeaponStat.ReloadTime);
        this.ReloadVariance = WeaponUpgrades.GetStat(mWeaponType, WeaponStat.ReloadVariance);
        this.Accuracy = WeaponUpgrades.GetStat(mWeaponType, WeaponStat.Accuracy);
        
        this.UpgradeWeapon(mLevel);
        Reset ();
    }
    
    protected virtual void UpgradeWeapon(int level)
    {
        float newDamage = (this.Damage * (1 + (float)level / 10f)); // 10% increase every level
        this.Damage = (int)(newDamage); 
        
        float newAccuracy = this.Accuracy + ((level-1) / 10) / 10f; // 10% increase every 10 levels
        this.Accuracy = newAccuracy; 
    }
}