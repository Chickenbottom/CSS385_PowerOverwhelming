using UnityEngine;
using System.Collections;

public class UnitSpawningTower : Tower {

	public SquadManager SquadManager;
	public GameObject Tent;
    // private SquadManager squads;

    void Start()
    {
        mTowerType = TowerType.kUnitSpawner;
    }

    public override void Click()
    {
    }
    
    public override void SetTarget(Vector3 location)
    {
		this.SquadManager.SetDestination(location);
    }
    
	public void SpawnUnit()
	{
		this.SquadManager.AddSquad(Tent.transform.position, this.UnitSpawnType);
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if (this.Allegiance == Allegiance.kRodelle)
			return;
			
		Unit unit = other.gameObject.GetComponent<Unit>();
				
		if (unit != null && unit.Squad.UnitType == UnitType.kPeasant) {
			Debug.Log ("Transforming unit!");
			unit.Squad.Spawn(this.Position, this.UnitSpawnType, Allegiance.kAI);
		}
	}
	
	public override Vector3 Position {
		get { return this.Tent.transform.position; }
	}
}
