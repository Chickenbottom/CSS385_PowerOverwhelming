using UnityEngine;
using System.Collections;

public class PeasantBehavior : MonoBehaviour {

	enum PEASANTCLASS{
		PEASANT,
		MELEE,
		RANGED,
		MAGIC,
		ELITE,
		BOSS,
	};

	PEASANTCLASS myClass;

	TowerBehavior MeleeTowerBehavior;
	TowerBehavior RangedTowerBehavior;
	TowerBehavior MagicTowerBehavior;
//	TowerBehavior EliteTowerBehavior;
//	TowerBehavior BossTowerBehavior;
	TowerBehavior HealTowerBehavior;

	GameObject MeleeTower;
	GameObject RangedTower;
	GameObject MagicTower;
	GameObject EliteTower;
	GameObject BossTower;
	GameObject HealTower;

	Camera mMainCamera;
	private Vector3 targetPos;
	private Vector2 mWorldMin;	// Better support 2D interactions
	private Vector2 mWorldMax;
	private Vector2 mWorldCenter;
	private Vector3 middleEarth = new Vector3(1f, 1f, 0f);
	private float peasantSpeed = 30f;
	private float convertDistance = 2f;
	private bool SiegeTower = false;
	// Use this for initialization
	void Start () {

		mMainCamera = Camera.main;

		myClass = PEASANTCLASS.PEASANT;
		MeleeTower = GameObject.Find("MeleeTower");
		RangedTower = GameObject.Find("RangedTower");
		MagicTower = GameObject.Find("MagicTower");
//		EliteTower = GameObject.Find("EliteTower");
//		BossTower = GameObject.Find("BossTower");
		HealTower = GameObject.Find("HealTower");


		MeleeTowerBehavior = MeleeTower.GetComponent<TowerBehavior>();
		RangedTowerBehavior = RangedTower.GetComponent<TowerBehavior>();
		MagicTowerBehavior = MagicTower.GetComponent<TowerBehavior>();
		//		EliteTowerBehavior = EliteTower.GetComponent<TowerBehavior>();
		//		BossTowerBehavior = BossTower.GetComponent<TowerBehavior>();
		UpdateWorldWindowBound();
		transform.position = new Vector3((float)(mWorldMin.x + mWorldMax.x), 
		                                 mWorldMin.y,0f);

		float myTarget = Random.value;
		if (myTarget < 0.3f)
			targetPos = MeleeTowerBehavior.getExactTowerLoc();
		else if (myTarget < 0.6f)
			targetPos = RangedTowerBehavior.getExactTowerLoc();
		else
			targetPos = MagicTowerBehavior.getExactTowerLoc();

		targetPos.z = 0f;

	}
	
	// Update is called once per frame
	void Update () {

        if (myClass == PEASANTCLASS.PEASANT)
        {
			SiegeTower = invadeTower(MagicTowerBehavior);
			if(!SiegeTower)
				SiegeTower = invadeTower(RangedTowerBehavior);
			if(!SiegeTower)
				SiegeTower = invadeTower(MeleeTowerBehavior);
		}
		else{
			targetPos = middleEarth;
		}

	    transform.up = targetPos - transform.position;
		if(SiegeTower || Vector3.Distance(targetPos,transform.position) > 20)
			transform.position += transform.up * (peasantSpeed * Time.smoothDeltaTime);
			
	}

	private bool invadeTower(TowerBehavior tower){
	 	if (tower.getTowerOwner() == TowerBehavior.TOWEROWNER.PEASANT)
		{
			if (Vector3.Distance(transform.position, tower.getExactTowerLoc()) < convertDistance)
			{
				myClass = setClass(tower);
				setColor();
				targetPos = middleEarth;
			}
			else
			{
				targetPos = tower.getExactTowerLoc();
			}
			return true;
		}
		return false;
	}
	private PEASANTCLASS setClass(TowerBehavior tower){
		switch(tower.getTowerType()){
		case TowerBehavior.TOWERTYPE.MAGICTOWER:
			return PEASANTCLASS.MAGIC;
			break;
		case TowerBehavior.TOWERTYPE.RANGEDTOWER:
			return PEASANTCLASS.RANGED;
			break;
		case TowerBehavior.TOWERTYPE.MELEETOWER:
			return PEASANTCLASS.MELEE;
			break;
		}
		return PEASANTCLASS.PEASANT;
	}
	private void setColor()
	{
		if(myClass == PEASANTCLASS.MELEE)
			this.renderer.material.color = Color.green;
		else if(myClass == PEASANTCLASS.RANGED)
			this.renderer.material.color = Color.red;
		else if(myClass == PEASANTCLASS.MAGIC)
			this.renderer.material.color = Color.blue;
	}	
	public void UpdateWorldWindowBound()
	{
		mWorldMax = MagicTower.transform.position;
		mWorldMin = MeleeTower.transform.position;
	}
	private bool withinWorldBound(){
		if(transform.position.x < mWorldMin.x ||
		   transform.position.x > mWorldMax.x ||
		   transform.position.y < mWorldMin.y ||
		   transform.position.y > mWorldMax.y)
			return true;
		else
			return false;
	}
}
