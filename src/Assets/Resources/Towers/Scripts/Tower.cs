using UnityEngine;
using System.Collections;

public enum TowerType {
	Ability,
	UnitSpawner
}

public abstract class Tower : Target
{
	///////////////////////////////////////////////////////////////////////////////////
	// Public Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
	public UnitType UnitSpawnType;
	public Renderer TowerSelector;
	public ProgressBar TowerHealthBar;
	
	public abstract void Click();
    
	/// <summary>
	/// Action performed when the user right-clicks on a location after
	/// selecting the tower
	/// </summary>
	/// <param name="location">Location. The game coodinate clicked on.</param>
	public abstract void SetTarget(Vector3 location);
    
	public int health;
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
        
        if (health < 0) {
			health = 8;
			Debug.Log ("Tower destroyed!");
			mAllegiance = this.Allegiance == Allegiance.Rodelle
				? Allegiance.AI
				: Allegiance.Rodelle;
				
			if (this.Allegiance != Allegiance.Rodelle)
				this.ShowSelector(false);
        }
        
        TowerHealthBar.Value = health;
    }
    
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	/////////////////////////////////////////////////////////////////////////////////// 
    void Awake()
    {
		health = 20;
		mAllegiance = Allegiance.Rodelle;
		TowerHealthBar.maxValue = health;
		TowerHealthBar.Value = health;
		this.ShowSelector(false);
    }
    
	void OnMouseDown()
	{
		if (this.Allegiance == Allegiance.Rodelle)
			GameObject.Find("GameManager").GetComponent<MouseManager>().Select(this);
	}
}
