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
	private Vector2 mWorldMin;	// Better support 2D interactions
	private Vector2 mWorldMax;
	private Vector2 mWorldCenter;
	private Vector3 middleEarth = new Vector3(1f, 1f, 0f);
	private float peasantSpeed = 30f;
	private float convertDistance = 2f;
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
		//transform.up = middleEarth - transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (myClass == PEASANTCLASS.PEASANT)
        {
            if (MagicTowerBehavior.getTowerOwner() == TowerBehavior.TOWEROWNER.PEASANT)
            {
				float temp = Vector3.Distance(transform.position, MagicTowerBehavior.getExactTowerLoc());
				if ( Vector3.Distance(transform.position, MagicTowerBehavior.getExactTowerLoc()) < convertDistance)
                {
                    myClass = PEASANTCLASS.MAGIC;
					this.renderer.material.color = Color.blue;
                    transform.up = middleEarth - transform.position;
                }
                else
                {
                    transform.up = MagicTowerBehavior.getExactTowerLoc() - transform.position;
                }
            }
            else if (RangedTowerBehavior.getTowerOwner() == TowerBehavior.TOWEROWNER.PEASANT)
            {
				if (Vector3.Distance(transform.position, RangedTowerBehavior.getExactTowerLoc()) < convertDistance)
                {
                    myClass = PEASANTCLASS.RANGED;
					this.renderer.material.color = Color.green;
                    transform.up = middleEarth - transform.position;
                }
                else
                {
                    transform.up = RangedTowerBehavior.getExactTowerLoc() - transform.position;
                }
            }
            else if (MeleeTowerBehavior.getTowerOwner() == TowerBehavior.TOWEROWNER.PEASANT)
            {
				if (Vector3.Distance(transform.position, MeleeTowerBehavior.getExactTowerLoc()) < convertDistance)
                {
                    myClass = PEASANTCLASS.MELEE;
					this.renderer.material.color = Color.red;
                    transform.up = middleEarth - transform.position;
                }
                else
                {
                    transform.up = MeleeTowerBehavior.getExactTowerLoc() - transform.position;
                }
            }
        }
		else
			transform.up = middleEarth - transform.position;

		transform.position += transform.up * (peasantSpeed * Time.smoothDeltaTime);
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
