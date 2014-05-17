using UnityEngine;
using System.Collections;

public class UnitSpawningTower : Tower {
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Inspector Presets
	///////////////////////////////////////////////////////////////////////////////////
	public SquadManager squadManager;
	public GameObject Tent;
    public UnitType UnitSpawnType;

	///////////////////////////////////////////////////////////////////////////////////
	// Public
	///////////////////////////////////////////////////////////////////////////////////
    
    public override void SetTarget(Vector3 location)
    {
		this.squadManager.SetDestination(location);
    }
    
	public void SpawnUnit()
	{
		if (squadManager.NumSquads() >= mMaxNumSquads)
			return;
			
		this.squadManager.AddSquad(Tent.transform.position, this.UnitSpawnType);
	}
	
	public override Vector3 Position {
		get { return this.Tent.transform.position; }
	}
	
	public override bool ValidMousePos(Vector3 mousePos)
	{
		return GameObject.Find("TargetFinder").GetComponent<ClickBox>().GetClickBoxBounds().Contains(mousePos);
	}
	
	///////////////////////////////////////////////////////////////////////////////////
	// Private
	///////////////////////////////////////////////////////////////////////////////////	
	
	private float mSpawnTime;
	private float mLastSpawnTime;
	private int mMaxNumSquads;
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	///////////////////////////////////////////////////////////////////////////////////
	
	void Update()
	{
		if (Time.time - mLastSpawnTime > mSpawnTime) {
			mLastSpawnTime = Time.time;
			this.SpawnUnit();
		}
	}
	
	protected override void Awake()
	{
		base.Awake();
		towerType = TowerType.UnitSpawner;
		mSpawnTime = GameState.SpawnTimes[this.UnitSpawnType];
		mLastSpawnTime = Time.time;
		mMaxNumSquads = 4; // TODO get from game state
	}
	
	void Start()
	{
		this.SpawnUnit();
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if (this.Allegiance == Allegiance.Rodelle)
			return;
			
		Unit unit = other.gameObject.GetComponent<Unit>();
				
		if (unit != null && unit.Squad.unitType == UnitType.Peasant) {
			unit.Squad.Spawn(Tent.transform.position, this.UnitSpawnType, Allegiance.AI);
		}
	}
}
