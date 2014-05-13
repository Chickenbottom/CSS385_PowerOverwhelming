﻿using UnityEngine;
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
		//Renderer r = TowerSelector.gameObject.GetComponent<Renderer>();
		//r.enabled = status;
	}

    public void Damage(int damage)
    {
        mHealth -= damage;
    }
    
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	/////////////////////////////////////////////////////////////////////////////////// 
    void Awake()
    {
		this.ShowSelector(false);
    }
    
	void OnMouseDown()
	{
		GameObject.Find("GameManager").GetComponent<MouseManager>().Select(this);
	}
}
