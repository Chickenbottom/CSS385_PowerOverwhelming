using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TowerType {
	Ability,
	UnitSpawner
}

public abstract class Tower : Target
{
	///////////////////////////////////////////////////////////////////////////////////
	// Inspector Presets
	///////////////////////////////////////////////////////////////////////////////////
	
	public Renderer TowerSelector;
	public ProgressBar TowerHealthBar;
	public List<Sprite> DamagedSprites;
	public Sprite CapturedSprite;

    public bool canTargetTowers;
	
	///////////////////////////////////////////////////////////////////////////////////
	// Public Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
    
    public abstract bool ValidMousePos(Vector3 mousePos);
    
	/// <summary>
	/// Action performed when the user right-clicks on a location after
	/// selecting the tower
	/// </summary>
	/// <param name="location">Location. The game coodinate clicked on.</param>
	public abstract void SetTarget(Vector3 location);
    
    public int MaxHealth = 20;
	public int health = 20;
	protected TowerType towerType;    
    
    public void ShowSelector(bool status)
    {
		if (this.TowerSelector == null)
			return;
			
		TowerSelector.enabled = status;
	}

    public override void Damage(int damage)
    {
        health -= damage;
        
        if (health <= 0) {
			health = 8;
			Debug.Log ("Tower destroyed!");
			mAllegiance = this.Allegiance == Allegiance.Rodelle
				? Allegiance.AI
				: Allegiance.Rodelle;
				
			if (this.Allegiance != Allegiance.Rodelle)
				this.ShowSelector(false);
        }
		UpdateAnimation();
        TowerHealthBar.Value = health;
    }
    
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
	
	private void UpdateAnimation()
	{
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		if (sr == null || DamagedSprites.Count == 0)
			return;
		
		if (this.Allegiance == Allegiance.AI)
			sr.sprite = CapturedSprite;
		else { 
			float percentDamaged = (float) (MaxHealth - health) / (float) MaxHealth;
			int spriteIndex =  (int)(percentDamaged * DamagedSprites.Count);
				
			sr.sprite = DamagedSprites[spriteIndex];
		}
	}
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	/////////////////////////////////////////////////////////////////////////////////// 
    void Awake()
    {
		mAllegiance = Allegiance.Rodelle;
		TowerHealthBar.maxValue = MaxHealth;
		TowerHealthBar.Value = health;
		this.ShowSelector(false);
		UpdateAnimation();
    }
    
	void OnMouseDown()
	{
        if (this.Allegiance == Allegiance.Rodelle)
        {
            GameObject.Find("GameManager").GetComponent<MouseManager>().TowerClicked(this);
        }	
    }
}
