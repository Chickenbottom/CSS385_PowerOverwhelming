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
			
		Squad squad = other.gameObject.GetComponent<Squad>();
				
		if (squad != null && squad.UnitType == UnitType.kPeasant) {
			Debug.Log ("Transforming unit!");
			squad.Spawn(this.transform.position, this.UnitSpawnType, Allegiance.kAI);
		}
	}
}
