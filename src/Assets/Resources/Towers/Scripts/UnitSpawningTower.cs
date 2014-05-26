using UnityEngine;
using System.Collections;

public class UnitSpawningTower : Tower
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Inspector Presets
    ///////////////////////////////////////////////////////////////////////////////////
    public SquadManager squadManager;
    public GameObject SpawnPoint;
    public UnitType UnitSpawnType;

    ///////////////////////////////////////////////////////////////////////////////////
    // Public
    ///////////////////////////////////////////////////////////////////////////////////
    
    public override void SetDestination (Vector3 location)
    {
        this.squadManager.SetDestination (location);
    }
    
    public override void UseTargetedAbility (Target target)
    {
        // does nothing
    }
    
    public void SpawnUnit ()
    {
        this.squadManager.AddSquad (SpawnPoint.transform.position, this.UnitSpawnType);
        
        if (mIsSelected)
            foreach (Squad s in squadManager.Squads)
                s.Select();
    }
   
    public override void Select ()
    {
        base.Select ();
        if (squadManager == null || squadManager.Squads == null)
            return;
        
        foreach (Squad s in squadManager.Squads)
            s.Select();
            
        mIsSelected = true;
    }
    
    public override void Deselect ()
    {
        base.Deselect ();
        
        if (squadManager == null || squadManager.Squads == null)
            return;
            
        foreach (Squad s in squadManager.Squads)
            s.Deselect();
            
        mIsSelected = false;
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    /////////////////////////////////////////////////////////////////////////////////// 
    
    private float mSpawnTime;
    private float mSpawnTimer;
    private int mMaxNumSquads;
    private bool mIsSelected;
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////
    
    void Update ()
    {
	    mSpawnTimer -= Time.deltaTime;
        
        // only spawn squads if you are able to
        if (squadManager.NumSquads () >= mMaxNumSquads)
            return;  
		
        // Immediately gain a squad if you retake a tower after a long enough time
        if (mSpawnTimer < 0 && this.Allegiance == Allegiance.Rodelle) {
            mSpawnTimer = mSpawnTime;
            this.SpawnUnit ();
        }
    }
    
    protected override void Awake ()
    {
        base.Awake ();
        towerType = TowerType.UnitSpawner;
        mSpawnTime = GameState.SpawnTimes [this.UnitSpawnType];
        mSpawnTimer = Time.time;
        mMaxNumSquads = 2; // TODO get from game state
    }
    
    void Start ()
    {
        this.SpawnUnit ();
        this.SpawnUnit ();
        mSpawnTimer = mSpawnTime;
    }
    
    void OnTriggerStay2D (Collider2D other)
    {
        if (this.Allegiance == Allegiance.Rodelle)
            return;
            
        Unit unit = other.gameObject.GetComponent<Unit> ();
                
        if (unit != null && unit.Squad.UnitType == UnitType.Peasant) {
            unit.Squad.NumSquadMembers = 2;
            unit.Squad.Spawn (SpawnPoint.transform.position, this.UnitSpawnType, Allegiance.AI);
        }
    }
}
