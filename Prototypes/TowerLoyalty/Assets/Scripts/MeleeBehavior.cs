using UnityEngine;
using System.Collections;

public class MeleeBehavior : MonoBehaviour {


	Vector2 targetPos;
	float kHeroSpeed = 50.0f;
	float attack_distance = 5.0f;
	float hit_points = 10;
	bool defeated = false;
	private bool ImThere = false;
	public MYTYPE troopType;
	public bool selected = false;
	GameManager gameManager = null;
	GameObject TargetTower = null;


	// Use this for initialization
	void Start () {
		if(gameManager == null)
			gameManager =  GameObject.Find("GameManager").GetComponent<GameManager>();
		targetPos = gameManager.getMeleeSpawnLoc();
		this.renderer.material.color = Color.red;
		
		//attack_distance *= attack_distance; //for finding the distance between an object and it's range a^2 + b^2
	}
	
	// Update is called once per frame
	void Update () {
		//if(targetPos == transform.position)
		//	return;

		if(defeated){
			transform.position += new Vector3(0f, 0f, -0.1f);
			if(transform.position.z <= transform.localScale.z)
				Destroy(gameObject);
			return;
		}
	//	float range = Vector2.SqrMagnitude(transform.position) - Vector2.SqrMagnitude(targetPos);
	//	range = Mathf.Sqrt(range);
		float range = 1000;

		if(ImThere){
		}

		if(Input.GetKey(KeyCode.Alpha1)){
			selected = true;
		//	gameManager.addTroop(gameObject);
		}
		if(!selected)
			if(Input.GetKey(KeyCode.Alpha2) || 
			   Input.GetKey(KeyCode.Alpha3) || 
			   Input.GetKey(KeyCode.Alpha4) ||
		   	   Input.GetKey(KeyCode.Alpha5)) {
					selected = false;		
		}


		if(Input.GetMouseButtonDown(1)){
			if(selected){
				//transform.position = Input.mousePosition;
				Vector2 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				gameManager.setMeleeSpawnLoc(tempPos);
				transform.up = tempPos;
				targetPos = tempPos;
				ImThere = false;
				selected = false;
			}

		}
		if(!ImThere){
			targetPos = gameManager.getMeleeSpawnLoc();
			Vector3 temp = new Vector3(targetPos.x, targetPos.y, 0f);
			transform.up = temp - transform.position;
			float dist = Vector3.Distance(transform.position, temp);	
				if(dist > 5f){
					transform.position += transform.up * (kHeroSpeed * Time.smoothDeltaTime);
				}
				else{
					ImThere = true;				
				}
		}
	}
	void OnMouseDown(){
		//gameManager.unselect();
		selected = true;
		gameManager.addTroop(gameObject);
	}


	public void setTroopType(MYTYPE c){
		troopType = c;
	}
	
	//void OnMouseDown(){
	//	if(selected)
	///		targetPos = Input.mousePosition;
	//}
}
