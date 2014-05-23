using UnityEngine;
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
    
    public override void SetTarget (Vector3 location)
    {
        // Do nothing
    }
                
    public void SpawnUnit ()
    {
        GameObject.Find("AI").GetComponent<EnemyAI>().AddSquad(this.transform.position);
    }
    
    public override Vector3 Position {
        get { return this.Tent.transform.position; }
    }
    
    public override bool ValidMousePos (Vector3 mousePos)
    {
        return GameObject.Find ("TargetFinder").GetComponent<ClickBox> ().GetClickBoxBounds ().Contains (mousePos);
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    /////////////////////////////////////////////////////////////////////////////////// 
    
    private float mSpawnTime;
    private float mLastSpawnTime;
    
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
            unit.Squad.NumSquadMembers = 1;
            unit.Squad.Spawn (Tent.transform.position, this.UnitSpawnType, Allegiance.AI);
        }
    }
    
    protected override void Awake ()
    {
        base.Awake ();
        this.Allegiance = Allegiance.Rodelle;
        towerType = TowerType.UnitSpawner;
        mSpawnTime = 30;
        mLastSpawnTime = Time.time;
        mHealth = 100;
    }
    
    void Start ()
    {
        GameObject.Find ("TargetFinder").GetComponent<TowerTargets> ().AddTower (this);
    }
}
