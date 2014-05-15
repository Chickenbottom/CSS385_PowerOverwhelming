using UnityEngine;
using System.Collections;



public class TowerBehavior : MonoBehaviour {
	
	public enum TOWEROWNER{
		RODELLE,
		PEASANT,
	};
	
	public enum TOWERTYPE{
		MELEETOWER,
		RANGEDTOWER,
		MAGICTOWER,
		HEALTOWER,
		ABILITYTOWER,
	};

	TOWEROWNER myOwner;
	public TOWERTYPE myTowerType;
	int hit_points;
	const int MAX_HIT_POINTS = 3;
	//GameObject myTower = null;
	GameManager gameManager = null;
	bool selected = false;
	Vector2 spawnLocation;
	const float BUTTON_INTER = .2f;
	float aButtonTime = 0f;
	float sButtonTime = 0f;
	float dButtonTime = 0f;
	float fButtonTime = 0f;
	// Use this for initialization
	void Start () {
		spawnLocation = transform.position;
		spawnLocation += new Vector2(Mathf.Sign(transform.position.x) * -1 * transform.localScale.x, 
		                             Mathf.Sign(transform.position.y) * -1 * transform.localScale.y);
		if(gameManager == null)
			GameObject.Find("GameManager").GetComponent<GameManager>();
		hit_points = MAX_HIT_POINTS;
		myOwner = TOWEROWNER.RODELLE;
		this.renderer.material.color = Color.green;
}
	
	// Update is called once per frame
	void Update () {



		if(hit_points < 0){
			if(myOwner == TOWEROWNER.RODELLE){
				myOwner = TOWEROWNER.PEASANT;
				this.renderer.material.color = Color.red;
				hit_points = MAX_HIT_POINTS;
			}
		   else{
				myOwner = TOWEROWNER.RODELLE;
				this.renderer.material.color = Color.green;
				hit_points = MAX_HIT_POINTS;
			}
	   }

		if(Input.GetMouseButtonUp(1)){
			if(selected){
				spawnLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				selected = false;
			}
		}
		if(Input.GetKey(KeyCode.A)){
			if(Time.realtimeSinceStartup - aButtonTime >  BUTTON_INTER){
				if(myTowerType == TOWERTYPE.MELEETOWER){
					hit_points--;
					aButtonTime = Time.realtimeSinceStartup;
				}
			}
		}
		if(Input.GetKey(KeyCode.S)){
			if(Time.realtimeSinceStartup - sButtonTime >  BUTTON_INTER){
				if(myTowerType == TOWERTYPE.RANGEDTOWER){
					hit_points--;
					sButtonTime = Time.realtimeSinceStartup;
				}
			}
		}
		if(Input.GetKey(KeyCode.D)){
			if(Time.realtimeSinceStartup - dButtonTime >  BUTTON_INTER){
				if(myTowerType == TOWERTYPE.MAGICTOWER){
					hit_points--;
					dButtonTime = Time.realtimeSinceStartup;
				}
			}
		}
		if(Input.GetKey(KeyCode.F)){
			if(Time.realtimeSinceStartup - fButtonTime >  BUTTON_INTER){		
				if(myTowerType == TOWERTYPE.HEALTOWER){
					hit_points--;
					fButtonTime = Time.realtimeSinceStartup;
				}
			}
		}
	}
	public void setSpawnLocation(Vector2 value){
		spawnLocation = value;
	}
	void OnTriggerEnter2D(Collider2D other){
		//other.transform.position;
	}
	void OnMouseDown(){
		selected = true;
	}

	void OnMouseOver(){
	}
	void hitTower(int hp){
		hit_points -= hp;
	}
	public Vector2 getTowerLocation(){
		Vector2 temp = transform.position;
		temp += new Vector2(Mathf.Sign(transform.position.x) * -1 * transform.localScale.x/2, 
		                    Mathf.Sign(transform.position.y) * -1 * transform.localScale.y/2);
		return temp;
	}
	public Vector3 getExactTowerLoc(){
		return transform.position;
	}
	public Vector2 getSpawnLocation(){
		return spawnLocation;
	}
	public TOWERTYPE getTowerType(){
		return myTowerType;
	}
	public TOWEROWNER getTowerOwner(){
		return myOwner;
	}

}
