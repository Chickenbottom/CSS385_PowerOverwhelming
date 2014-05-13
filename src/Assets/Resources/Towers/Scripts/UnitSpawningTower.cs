using UnityEngine;
using System.Collections;

public class UnitSpawningTower : Tower {

	public SquadManager SquadManager;
	public GameObject Tent;
    // private SquadManager squads;

    void Start()
    {
        mTowerType = TowerType.kUnitSpawner;
        mHealth = 100;
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

}
