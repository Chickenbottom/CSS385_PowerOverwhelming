using UnityEngine;
using System.Collections;

public class UnitSpawningTower : Tower {

	public SquadManager squadManager;
	public GameObject tent;
    public UnitType UnitSpawnType;
    // private SquadManager squads;

    void Start()
    {
        towerType = TowerType.UnitSpawner;
    }
    
    public override void SetTarget(Vector3 location)
    {
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
			Debug.Log ("Transforming unit!");
			unit.Squad.Spawn(this.Position, this.UnitSpawnType, Allegiance.AI);
		}
	}
	
	public override Vector3 Position {
		get { return this.tent.transform.position; }
	}

    public override bool ValidMousePos(Vector3 mousePos)
    {
        return GameObject.Find("TargetFinder").GetComponent<ClickBox>().GetClickBoxBounds().Contains(mousePos);
    }

}
