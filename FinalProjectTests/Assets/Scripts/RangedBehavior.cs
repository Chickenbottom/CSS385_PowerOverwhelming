using UnityEngine;
using System.Collections;

public class RangedBehavior : MonoBehaviour {

	Vector3 targetPos;
	float kHeroSpeed = 50.0f;
	float attack_distance = 2.0f;
	private bool ImThere = true;
	public MYTYPE troopType;
	public bool selected = false;
	GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	
	// Use this for initialization
	void Start () {
		//targetPos = new Vector3(0.001f,0.001f,0.0f);
		attack_distance *= attack_distance;
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
				targetPos = tempPos;
				ImThere = false;
				selected = false;
			}
		}
		if(Input.GetKey(KeyCode.Alpha2)){ //ranged
			if(troopType == MYTYPE.RANGED){
				selected = true;
				this.renderer.material.color = Color.green;
			}
		}
		if(Input.GetKey(KeyCode.Alpha1) || 
		   Input.GetKey(KeyCode.Alpha3) || 
		   Input.GetKey(KeyCode.Alpha4) ||
		   Input.GetKey(KeyCode.Alpha5)) {
			selected = false;		
		}
		if(!ImThere){
			float range = Vector3.SqrMagnitude(transform.position) + Vector3.SqrMagnitude(targetPos);
			transform.up = targetPos - transform.position;
			if(range > attack_distance){
				transform.position += transform.up * (kHeroSpeed * Time.smoothDeltaTime);
			}
			else{
				ImThere = true;		
				this.renderer.material.color = Color.red;
			}
		}
	}
	void OnMouseUp(){
		//gameManager.unselect();
		selected = true;
		this.renderer.material.color = Color.red;
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
