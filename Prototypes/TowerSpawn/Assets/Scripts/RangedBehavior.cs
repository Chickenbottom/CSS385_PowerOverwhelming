using UnityEngine;
using System.Collections;

public class RangedBehavior : MonoBehaviour {

	Vector3 targetPos;
	float kHeroSpeed = 50.0f;
	float attack_distance = 2.0f;
	private bool ImThere = false;
	public MYTYPE troopType;
	public bool selected = false;
	GameManager gameManager; 
	
	// Use this for initialization
	void Start () {
		if(gameManager == null)
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		targetPos = gameManager.getRangedSpawnLoc();
		attack_distance *= attack_distance;
		this.renderer.material.color = Color.green;
		
	}
	
	// Update is called once per frame
	void Update () {
		//if(targetPos == transform.position)
		//	return;

	//	range = Mathf.Sqrt(range);
		//float range = 1000;


		if(Input.GetMouseButtonDown(1)){
			if(selected){
				//transform.position = Input.mousePosition;
				Vector2 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				transform.up = tempPos;
				gameManager.setRangedSpawnLoc(tempPos);
				targetPos = tempPos;
				ImThere = false;
				selected = false;
			}
		}
		if(Input.GetKey(KeyCode.Alpha2)){ //ranged
			if(troopType == MYTYPE.RANGED){
				selected = true;
			}
		}
		if(!selected)
			if(Input.GetKey(KeyCode.Alpha1) || 
			   Input.GetKey(KeyCode.Alpha3) || 
			   Input.GetKey(KeyCode.Alpha4) ||
		   	   Input.GetKey(KeyCode.Alpha5)) {
				selected = false;		
		}
		if(!ImThere){
			targetPos = gameManager.getRangedSpawnLoc();
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
		//gameManager.unselect();
		selected = true;
	}
//	void targetPosition(Vector3 target_pos){
//		targetPos = target_pos;
//		Vector3 dir = target_pos - transform.position;
//		transform.up = dir;
//	}

	public void setTroopType(MYTYPE c){
		troopType = c;
	}
	
	//void OnMouseDown(){
	//	if(selected)
	///		targetPos = Input.mousePosition;
	//}
}
