using UnityEngine;
using System.Collections;

public abstract class TowerBehavior : MonoBehaviour {

	abstract public void Click();
	abstract public TOWERTYPE getMyTowerType();

	public enum TOWERTYPE{
		MELEE,
		RANGED,
		MAGIC,
		HEAL,
		NONE,
	};

	AbilityTowerBehavior abilityTower = null;
	UnitSpawnTower spawnTower = null;
	TOWERTYPE selectedTower;
	// Use this for initialization
	void Start () {
		selectedTower = TOWERTYPE.NONE;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonUp(0)){
			if(selectedTower != TOWERTYPE.NONE && 
			   selectedTower != TOWERTYPE.HEAL && 
			   spawnTower != null){
				Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				//spawnTower.setDestination(destination);
			}
		}
	}
	public void setSelected(AbilityTowerBehavior AT = null, UnitSpawnTower ST = null){
		if(selectedTower == TOWERTYPE.NONE){
			if(AT == null){
				spawnTower = ST;
				selectedTower = ST.getMyTowerType();
			}
			else{
				abilityTower = AT;
				selectedTower = ST.getMyTowerType();
			}
		}

		if(selectedTower == TOWERTYPE.HEAL)
			if(ST != null){
				//ST.healTower(AT.getHealRate);
				clearSelected();
			}
	}
	public void clearSelected(){
		selectedTower = TOWERTYPE.NONE;
		abilityTower = null;
		spawnTower = null;
	}
}
