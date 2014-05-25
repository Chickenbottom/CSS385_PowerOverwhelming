using UnityEngine;
using System.Collections;

public class UnitSpawningTower : Tower
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Inspector Presets
    ///////////////////////////////////////////////////////////////////////////////////
    public SquadManager squadManager;
    public GameObject Tent;
    public UnitType UnitSpawnType;

    ///////////////////////////////////////////////////////////////////////////////////
    // Public
    ///////////////////////////////////////////////////////////////////////////////////
    
    public override void SetTarget (Vector3 location)
    {
        this.squadManager.SetDestination (location);
    }
    
    public void SpawnUnit ()
    {
        this.squadManager.AddSquad (Tent.transform.position, this.UnitSpawnType);
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
    private float mSpawnTimer;
    private int mMaxNumSquads;
    
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
        // start with full squad size
        this.SpawnUnit ();
        this.SpawnUnit (); 
        mSpawnTimer = mSpawnTime;
        GameObject.Find ("TargetFinder").GetComponent<TowerTargets> ().AddTower (this);
    }
    
    void OnTriggerStay2D (Collider2D other)
    {
        if (this.Allegiance == Allegiance.Rodelle)
            return;
            
        Unit unit = other.gameObject.GetComponent<Unit> ();
                
        if (unit != null && unit.Squad.UnitType == UnitType.Peasant) {
            unit.Squad.NumSquadMembers = 2;
            unit.Squad.Spawn (Tent.transform.position, this.UnitSpawnType, Allegiance.AI);
        }
    }
}
