using UnityEngine;
using System.Collections;

public enum TowerType {
	kAbility,
	kUnitSpawner
}

public abstract class Tower : Target, IDamagable 
{
	///////////////////////////////////////////////////////////////////////////////////
	// Public Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
	public UnitType UnitSpawnType;
	public Renderer TowerSelector;
	
	public abstract void Click();
    
	/// <summary>
	/// Action performed when the user right-clicks on a location after
	/// selecting the tower
	/// </summary>
	/// <param name="location">Location. The game coodinate clicked on.</param>
	public abstract void SetTarget(Vector3 location);
    
	protected int mHealth;
	protected TowerType mTowerType;    
    
    public void ShowSelector(bool status)
    {
		if (this.TowerSelector == null)
			return;
			
		TowerSelector.enabled = status;
	}

    public void Damage(int damage)
    {
        mHealth -= damage;
        
        if (mHealth < 0) {
			mHealth = 10;
			Debug.Log ("Tower destroyed!");
			mAllegiance = this.Allegiance == Allegiance.kRodelle
				? Allegiance.kAI
				: Allegiance.kRodelle;
				
			if (this.Allegiance != Allegiance.kRodelle)
				this.ShowSelector(false);
        }
        
        Debug.Log ("Tower Health: " + mHealth);
    }
    
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	/////////////////////////////////////////////////////////////////////////////////// 
    void Awake()
    {
		mHealth = 10;
		mAllegiance = Allegiance.kRodelle;
		this.ShowSelector(false);
    }
    
	void OnMouseDown()
	{
		if (this.Allegiance == Allegiance.kRodelle)
			GameObject.Find("GameManager").GetComponent<MouseManager>().Select(this);
	}
}
