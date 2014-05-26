﻿using UnityEngine;
using System.Collections;

public class EnemySpawningTower : Tower
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Inspector Presets
    ///////////////////////////////////////////////////////////////////////////////////
    public GameObject Tent;
    public UnitType UnitSpawnType;
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Public
    ///////////////////////////////////////////////////////////////////////////////////

    public override void SetDestination (Vector3 destination)
    {
        // does nothing
    }
    
    public override void UseTargetedAbility (Target Target)
    {
        // does nothing
    }

    public void SpawnUnit ()
    {
		if (mUnitCount == 0)
			return;

		int squadSize = Mathf.Min (mUnitCount, 4);

		mUnitCount -= squadSize;
        GameObject.Find("AI").GetComponent<EnemyAI>().AddSquad(squadSize, this.transform.position);
    }
    
    public override Vector3 Position {
        get { return this.Tent.transform.position; }
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    /////////////////////////////////////////////////////////////////////////////////// 
    
    private float mSpawnTime;
    private float mLastSpawnTime;
	private int mUnitCount;
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////
    
    void Update ()
    {
        // Only spawns squads if the enemy owns the tower
        if (this.Allegiance == Allegiance.Rodelle)
            return;
        
        if (Time.time - mLastSpawnTime > mSpawnTime) {
            mLastSpawnTime = Time.time;
            this.SpawnUnit ();
        }
    }
    
    void OnTriggerStay2D (Collider2D other)
    {
        if (this.Allegiance == Allegiance.Rodelle)
            return;
        
        Unit unit = other.gameObject.GetComponent<Unit> ();
        
        if (unit != null && unit.Squad.UnitType == UnitType.Peasant) {
			unit.Damage(unit.MaxHealth);
			mUnitCount ++;
        }
    }
    
    protected override void Awake ()
    {
        base.Awake ();
        this.Allegiance = Allegiance.Rodelle;
        towerType = TowerType.UnitSpawner;
        mSpawnTime = 3;
        mLastSpawnTime = Time.time;
        mHealth = 100;
    }
}
