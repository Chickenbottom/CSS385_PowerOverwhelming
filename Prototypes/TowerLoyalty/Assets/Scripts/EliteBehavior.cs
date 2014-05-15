using UnityEngine;
using System.Collections;

public class EliteBehavior : MonoBehaviour {

	Vector3 targetPos;
	float kHeroSpeed = 50.0f;
	float attack_distance = 15;
	private bool ImThere = false;
	public MYTYPE troopType;
	public bool selected = false;
	GameManager gameManager = null;
	// Use this for initialization
	void Start () {
		if(gameManager == null)
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
		targetPos = gameManager.getEliteSpawnLoc();
		attack_distance *= attack_distance;
		this.renderer.material.color = Color.cyan;		
	}
	
	// Update is called once per frame
	void Update () {
		//if(targetPos == transform.position)
		//	return;

	//	float range = Vector3.SqrMagnitude(transform.position) - Vector3.SqrMagnitude(targetPos);
	//	range = Mathf.Sqrt(range);
		float range = 1000;



		if(ImThere){
		}

		if(Input.GetKey(KeyCode.Alpha4)){ //elite 
				selected = true;
		}
		if(!selected)
			if(Input.GetKey(KeyCode.Alpha1) || 
			   Input.GetKey(KeyCode.Alpha2) || 
			   Input.GetKey(KeyCode.Alpha3) ||
			   Input.GetKey(KeyCode.Alpha5)) {
			selected = false;		
		}

		if(Input.GetMouseButtonDown(1)){
			if(selected){
				//transform.position = Input.mousePosition;
				Vector2 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				gameManager.setEliteSpawnLoc(tempPos);
				transform.up = tempPos;
				targetPos = tempPos;
				ImThere = false;
			}
		}
		if(!ImThere){
			Vector3 temp = new Vector3(targetPos.x, targetPos.y, 0f);
			transform.up = temp - transform.position;
			float dist = Vector3.Distance(transform.position, targetPos);	
			if(dist > 5f){
				transform.position += transform.up * (kHeroSpeed * Time.smoothDeltaTime);
			}
			else{
				ImThere = true;				
			}
		}
	}
	void OnMouseUp(){
		selected = true;
	}


	public void setTroopType(MYTYPE c){
		troopType = c;
	}
	
}
