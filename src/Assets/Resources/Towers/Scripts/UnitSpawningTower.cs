using UnityEngine;
using System.Collections;

public class UnitSpawningTower : Tower {

	public SquadManager squadManager;
	public GameObject tent;
    public UnitType UnitSpawnType;
    // private SquadManager squads;

	private Bounds mGameArea;

    void Start()
    {
        towerType = TowerType.UnitSpawner;
    }
    
	// no targeted abilities. Can add force move here?
	public override void UseTargetedAbility(Target target) {} 
	
	// no positional abilities. Alternate way to set rally point?
	public override void UsePositionalAbility(Vector3 position) 
	{
		this.SetTarget(position);
	} 
	
    public override void SetTarget(Vector3 location)
    {
		// can only send squads to game area
		if (!mGameArea.Contains(location))
			return; 
			
		this.squadManager.SetDestination(location);
    }
    
	public void SpawnUnit()
	{
		this.squadManager.AddSquad(tent.transform.position, this.UnitSpawnType);
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if (this.Allegiance == Allegiance.Rodelle)
			return;
			
		Unit unit = other.gameObject.GetComponent<Unit>();
				
		if (unit != null && unit.Squad.unitType == UnitType.Peasant) {
			unit.Squad.Spawn(this.Position, this.UnitSpawnType, Allegiance.AI);
		}
	}
	
	public override Vector3 Position {
		get { return this.tent.transform.position; }
	}

	protected override void Awake()
	{
		base.Awake();
		GameObject gameBounds = GameObject.Find ("ClickBox");
		BoxCollider2D b = gameBounds.GetComponent<BoxCollider2D>();
		mGameArea = new Bounds(new Vector3(b.center.x, b.center.y, 0f), new Vector3(b.size.x, b.size.y, 0f));
	}
}
