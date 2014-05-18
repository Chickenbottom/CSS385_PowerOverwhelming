using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TowerType
{
    Ability,
    UnitSpawner
}

public abstract class Tower : Target
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Inspector Presets
    ///////////////////////////////////////////////////////////////////////////////////
    
    public Renderer TowerSelector;
    public Progressbar TowermHealthBar;
    public List<Sprite> DamagedSprites;
    public Sprite CapturedSprite;
    public bool canTargetTowers;
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Public Methods and Variables
    ///////////////////////////////////////////////////////////////////////////////////
    
    public abstract bool ValidMousePos (Vector3 mousePos);
    
    /// <summary>
    /// Action performed when the user right-clicks on a location after
    /// selecting the tower
    /// </summary>
    /// <param name="location">Location. The game coodinate clicked on.</param>
    public abstract void SetTarget (Vector3 location);
    
    protected TowerType towerType;
    
    public void ShowSelector (bool status)
    {
        if (this.TowerSelector == null)
            return;
            
        TowerSelector.enabled = status;
    }

    public override void Damage (int damage)
    {
        if (damage > 1) // makes towers stronger
            damage = 1;
            
        mHealth -= damage;
        
        if (mHealth <= 0) {
            mHealth = (int)(mMaxHealth * 0.25);
            mAllegiance = this.Allegiance == Allegiance.Rodelle
                ? Allegiance.AI
                : Allegiance.Rodelle;
                
            if (this.Allegiance != Allegiance.Rodelle)
                this.ShowSelector (false);
        }
        
        if (mHealth > mMaxHealth)
            mHealth = mMaxHealth;
        TowermHealthBar.Value = mHealth;
        UpdateAnimation ();
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private Methods and Variables
    ///////////////////////////////////////////////////////////////////////////////////
    
    private void UpdateAnimation ()
    {
        SpriteRenderer sr = this.GetComponent<SpriteRenderer> ();
        if (sr == null || DamagedSprites.Count == 0)
            return;
        
        if (this.Allegiance == Allegiance.AI)
            sr.sprite = CapturedSprite;
        else { 
            float percentDamaged = (float)(mMaxHealth - mHealth) / (float)mMaxHealth;
            int spriteIndex = (int)(percentDamaged * DamagedSprites.Count);
            if (spriteIndex < DamagedSprites.Count)
                sr.sprite = DamagedSprites [spriteIndex];
        }
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    /////////////////////////////////////////////////////////////////////////////////// 
    protected virtual void Awake ()
    {
        mAllegiance = Allegiance.Rodelle;
        
        mHealth = 45;
        mMaxHealth = 45;
        
        TowermHealthBar.MaxValue = mMaxHealth;
        TowermHealthBar.Value = mHealth;
        this.ShowSelector (false);
        UpdateAnimation ();
    }
    
    void OnMouseDown ()
    {
        if (this.Allegiance == Allegiance.Rodelle) {
            GameObject.Find ("GameManager").GetComponent<MouseManager> ().TowerClicked (this);
        }   
    }
}
