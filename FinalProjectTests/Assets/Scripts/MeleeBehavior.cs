using UnityEngine;
using System.Collections;

public class MeleeBehavior : MonoBehaviour {

	Vector3 targetPos;
	float kHeroSpeed = 50.0f;
	float attack_distance = 5.0f;
	private bool ImThere = true;
	public MYTYPE troopType;
	public bool selected = false;
	GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	// Use this for initialization
	void Start () {
		targetPos = new Vector3(0.001f,0.001f,0.0f);
		//attack_distance *= attack_distance; //for finding the distance between an object and it's range a^2 + b^2
	}
	
	// Update is called once per frame
	void Update () {
		//if(targetPos == transform.position)
		//	return;

	//	float range = Vector2.SqrMagnitude(transform.position) - Vector2.SqrMagnitude(targetPos);
	//	range = Mathf.Sqrt(range);
		float range = 1000;
		if(ImThere){
			this.renderer.material.color = Color.red;
		}

		if(Input.GetKey(KeyCode.Alpha1)){
			selected = true;
			this.renderer.material.color = Color.green;
		}
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
				transform.up = tempPos;
				targetPos = tempPos;
				ImThere = false;
				selected = false;
			}
		}
		if(!ImThere){
			transform.up = targetPos - transform.position;
				if(attack_distance < range){
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
