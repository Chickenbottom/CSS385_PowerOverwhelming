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
    
    private int mGarrisonedPeasants = 0;
    private float mEnemySpawnTime = 3.0f;
    private float mEnemySpawnTimer;
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////
    
    void Update ()
    {
	    UpdateFriendlySpawnTimer();
        UpdateEnemySpawnTimer();
    }
    
    private void UpdateFriendlySpawnTimer ()
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
    
    private void UpdateEnemySpawnTimer ()
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
    
    private void SpawnEnemyUnit()
    {
        if (mGarrisonedPeasants <= 0)
            return;
        
        int squadSize = Mathf.Min (mGarrisonedPeasants, 3);
        
        mGarrisonedPeasants -= (squadSize + 1); // lose a peasant when being armed
        
        GameObject.Find ("AI").GetComponent<EnemyAI> ().AddSquad (squadSize, this.transform.position, this.UnitSpawnType);
    }
    
    protected override void Awake ()
    {
        base.Awake ();
        towerType = TowerType.UnitSpawner;
        mSpawnTime = GameState.SpawnTimes [this.UnitSpawnType];
        mSpawnTimer = Time.time;
        mMaxNumSquads = 2; // TODO get from game state
        mEnemySpawnTimer = mEnemySpawnTime;
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
            unit.Damage (unit.MaxHealth);
            mGarrisonedPeasants ++;
        }
    }
}
