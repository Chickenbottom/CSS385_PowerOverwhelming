using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawningTower : Tower
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Inspector Presets
    ///////////////////////////////////////////////////////////////////////////////////
    public UnitType UnitSpawnType;
    public List<GameObject> SpawnPoints;
    public List<GameObject> SpawnWaypoint;
    
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

    private void SpawnEnemyUnit()
    {
        if (mGarrisonedPeasants <= 0)
            return;
        
        int squadSize = Mathf.Min (mGarrisonedPeasants, 3);
        
        mGarrisonedPeasants -= (squadSize + 1); // lose a peasant when being armed
        
        GameObject.Find ("AI").GetComponent<EnemyAI> ().AddSquad (squadSize, this.transform.position);
    }
   
    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    /////////////////////////////////////////////////////////////////////////////////// 
    
    private float mEnemySpawnTime = 3; // 3 seconds for peasants to arm themselves
    private float mEnemySpawnTimer;
    private int mGarrisonedPeasants;
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////
    
    void Update ()
    {        
        // Only spawns squads if the enemy owns the tower
        if (this.Allegiance == Allegiance.Rodelle) {
            // reset the spawn timer and number of peasants
            mGarrisonedPeasants = 0;
            mEnemySpawnTimer = mEnemySpawnTime;
            return;
        }
        
        mEnemySpawnTimer -= Time.deltaTime;
        
        if (mEnemySpawnTimer < 0) {
            mEnemySpawnTimer = mEnemySpawnTime;
            this.SpawnEnemyUnit ();
        }
    }
    
    void OnTriggerStay2D (Collider2D other)
    {
        if (this.Allegiance == Allegiance.Rodelle)
            return;
        
        Unit unit = other.gameObject.GetComponent<Unit> ();
        
        if (unit != null && unit.Squad.UnitType == UnitType.Peasant) {
            unit.Damage (unit.MaxHealth);
            mGarrisonedPeasants ++;
        }
    }
    
    protected override void Awake ()
    {
        base.Awake ();
        this.Allegiance = Allegiance.Rodelle;
        towerType = TowerType.UnitSpawner;
        mEnemySpawnTimer = mEnemySpawnTime;
    }
}
