using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	#region Variables

	ArrayList selectables = new ArrayList();
	Vector2 start_Mouse;
	Vector3 selection_Start;
	MeleeBehavior meleeTrooper;
	RangedBehavior rangedTrooper;
	MagicBehavior magicTrooper;
	EliteBehavior eliteTrooper;
	BossBehavior bossTrooper;
	ArrayList selectedTroops = new ArrayList();
	GameObject curTower = null;
	bool towerSelected = false;
	public GameObject Melee = null;
	public GameObject Ranged = null;
	public GameObject Magic = null;
	public GameObject Elite = null;
	public GameObject Boss = null;
	public GameObject Peasant = null;

	private const float MELEE_SPAWN_INTER = 1f;
	private const float RANGED_SPAWN_INTER = 2f;
	private const float MAGIC_SPAWN_INTER = 3f;
	private const float ELITE_SPAWN_INTER = 40f;
	private const float BOSS_SPAWN_INTER = 60f;
	private const float PEASANT_SPAWN_INTER = 10f;

	private float previousMeleeSpawn = 0f;
	private float previousRangedSpawn = 0f;
	private float previousMagicSpawn = 0f;
	private float previousEliteSpawn = 0f;
	private float previousBossSpawn = 0f;
	private float previousPeasantSpawn = 0f;

	Vector2 meleeSpawnLocation;
	Vector2 rangedSpawnLocation;
	Vector2 magicSpawnLocation;
	Vector2 eliteSpawnLocation;
	Vector2 bossSpawnLocation;

	#endregion

	// Use this for initialization
	void Start () {

		if (null == Melee)
			Melee = Resources.Load("Prefabs/Melee") as GameObject;
		if (null == Ranged)
			Ranged = Resources.Load("Prefabs/Ranged") as GameObject;
		if (null == Magic)
			Magic = Resources.Load("Prefabs/Magic") as GameObject;
		if (null == Elite)
			Elite = Resources.Load("Prefabs/Elite") as GameObject;
		if (null == Boss)
			Boss = Resources.Load("Prefabs/Boss") as GameObject;
		if(null == Peasant)
			Peasant = Resources.Load("Prefabs/Peasant") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {

	//	spawnPeasant();
		spawnMelee();
		spawnRanged();
		spawnMagic();
	//	spawnElite();
	//	spawnBoss();


	}
	public void addTroop(GameObject t){
		selectedTroops.Add(t);
	}

	void spawnPeasant(){
		if(Time.realtimeSinceStartup - previousPeasantSpawn > PEASANT_SPAWN_INTER){
				GameObject e = Instantiate(Peasant) as GameObject;
				previousPeasantSpawn = Time.realtimeSinceStartup;
		}
	}
	void spawnMelee(){
		if(Time.realtimeSinceStartup - previousMeleeSpawn > MELEE_SPAWN_INTER){
			TowerBehavior tower = GameObject.Find("MeleeTower").GetComponent<TowerBehavior>();
			if(tower.getTowerOwner() == TowerBehavior.TOWEROWNER.RODELLE){
				GameObject e = Instantiate(Melee) as GameObject;
				MeleeBehavior soldier = e.GetComponent<MeleeBehavior>(); // Shows how to get the script from GameObject
				previousMeleeSpawn = Time.realtimeSinceStartup;
				if(soldier != null){
					e.transform.position = tower.getTowerLocation();
				//	e.transform.position += new Vector3(10f,10f,0f);
					e.transform.up = tower.getSpawnLocation();
					meleeSpawnLocation = tower.getSpawnLocation();
				}
			}
		}
	}
	void spawnRanged(){
		if(Time.realtimeSinceStartup - previousRangedSpawn > RANGED_SPAWN_INTER){
			TowerBehavior tower = GameObject.Find("RangedTower").GetComponent<TowerBehavior>();
			if(tower.getTowerOwner() == TowerBehavior.TOWEROWNER.RODELLE){
				GameObject e = Instantiate(Ranged) as GameObject;
				RangedBehavior soldier = e.GetComponent<RangedBehavior>(); // Shows how to get the script from GameObject
				previousRangedSpawn = Time.realtimeSinceStartup;
				if(soldier != null){
					e.transform.position = tower.getTowerLocation();
				//	e.transform.position += new Vector3(10f,-10f,0f);
					e.transform.up = tower.getSpawnLocation();
					rangedSpawnLocation = tower.getSpawnLocation();
				}
			}
		}
	}
	void spawnMagic(){
		if(Time.realtimeSinceStartup - previousMagicSpawn > MAGIC_SPAWN_INTER){
			TowerBehavior tower = GameObject.Find("MagicTower").GetComponent<TowerBehavior>();
			if(tower.getTowerOwner() == TowerBehavior.TOWEROWNER.RODELLE){	
				GameObject e = Instantiate(Magic) as GameObject;
				MagicBehavior soldier = e.GetComponent<MagicBehavior>(); // Shows how to get the script from GameObject
				previousMagicSpawn = Time.realtimeSinceStartup;
				if(soldier != null){
					e.transform.position = tower.getTowerLocation();
				//	e.transform.position += new Vector3(-10f,-10f,0f);
					e.transform.up = tower.getSpawnLocation();
					magicSpawnLocation = tower.getSpawnLocation();
				}
			}
		}
	}
	void spawnElite(){
		if(Time.realtimeSinceStartup - previousEliteSpawn > ELITE_SPAWN_INTER){
			TowerBehavior tower = GameObject.Find("EliteTower").GetComponent<TowerBehavior>();
			if(tower.getTowerOwner() == TowerBehavior.TOWEROWNER.RODELLE){		
				GameObject e = Instantiate(Elite) as GameObject;
				EliteBehavior soldier = e.GetComponent<EliteBehavior>(); // Shows how to get the script from GameObject
				previousEliteSpawn = Time.realtimeSinceStartup;
				if(soldier != null){
					e.transform.position = tower.getTowerLocation();
					e.transform.up = tower.getSpawnLocation();
					eliteSpawnLocation = tower.getSpawnLocation();
				}
			}
		}
	}
	void spawnBoss(){
		if(Time.realtimeSinceStartup - previousBossSpawn > BOSS_SPAWN_INTER){
			TowerBehavior tower = GameObject.Find("BossTower").GetComponent<TowerBehavior>();
			if(tower.getTowerOwner() == TowerBehavior.TOWEROWNER.RODELLE){			
				GameObject e = Instantiate(Boss) as GameObject;
				BossBehavior soldier = e.GetComponent<BossBehavior>(); // Shows how to get the script from GameObject
				previousBossSpawn = Time.realtimeSinceStartup;
				if(soldier != null){
					e.transform.position = tower.getTowerLocation();
					e.transform.up = tower.getSpawnLocation();
					 bossSpawnLocation = tower.getSpawnLocation();
				}
			}
		}
	}
	public void setCurTower(GameObject ct){
		curTower = ct;
	}
	public GameObject getCurTower(){
		return curTower;
	}
	public Vector3 getTowerPos(){
		if(curTower != null)
			return curTower.transform.position;
		else 
			return new Vector3();
	}
	public bool getTowerSelected(){
		return towerSelected;
	}
	public Vector2 getMeleeSpawnLoc(){
		return meleeSpawnLocation;
	}
	public void setMeleeSpawnLoc(Vector2 value){
		meleeSpawnLocation = value;
		TowerBehavior tower = GameObject.Find("MeleeTower").GetComponent<TowerBehavior>();
		tower.setSpawnLocation(value);	

	}
	public Vector2 getRangedSpawnLoc(){
		return rangedSpawnLocation;
	}
	public void setRangedSpawnLoc(Vector2 value){
		rangedSpawnLocation = value;
		TowerBehavior tower = GameObject.Find("RangedTower").GetComponent<TowerBehavior>();
		tower.setSpawnLocation(value);	
	}
	public Vector2 getMagicSpawnLocation(){
			return magicSpawnLocation;
		}
	public void setMagicSpawnLoc(Vector2 value){
		magicSpawnLocation = value;
		TowerBehavior tower = GameObject.Find("MagicTower").GetComponent<TowerBehavior>();
		tower.setSpawnLocation(value);	
	}
	public Vector2 getEliteSpawnLoc(){
			return eliteSpawnLocation;
		}
	public void setEliteSpawnLoc(Vector2 value){
		eliteSpawnLocation = value;
		TowerBehavior tower = GameObject.Find("EliteTower").GetComponent<TowerBehavior>();
		tower.setSpawnLocation(value);			
	}
	public Vector2 getBossSpawnLoc(){
			return bossSpawnLocation;
		}
	public void setBossSpawnLoc(Vector2 value){
		bossSpawnLocation = value;
		TowerBehavior tower = GameObject.Find("BossTower").GetComponent<TowerBehavior>();
		tower.setSpawnLocation(value);	
		}
}