using UnityEngine;
using System.Collections;

public class UnitSpawningTower : Tower {

	public SquadManager SquadManager;
	public GameObject Tent;
    // private SquadManager squads;

    void Start()
    {
        type = TowerType.kUnitSpawner;
        health = 100;
    }

    public override void Click()
    {
        Debug.Log("YAY IT WORKED" + health + "  " + type);
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
