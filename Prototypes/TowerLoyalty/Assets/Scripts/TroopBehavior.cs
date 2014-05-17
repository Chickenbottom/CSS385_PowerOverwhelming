using UnityEngine;
using System.Collections;

//https://www.youtube.com/watch?v=szmChuK4tKQ

public enum MOVEMENTACTION{
	ATTACK, //travels to the target, ignores other enemies 
	DEFEND, // will attack nearby enemies but does not leave their position 
	MOVE, //travels to the target position, but will fight along the way
	STATIONARY, // has reach target posotion but has not received further directions
};

public enum MYTYPE{
	MELEE, 
	RANGED,
	WIZARD,
	ELITE,// wish list
	BOSS, // wish list
};

public enum MYSTATE{
	ATTACKING,
	DEFENDING,
	IDLE,
	MOVING,
};

public class TroopBehavior : MonoBehaviour {

	//Vector3 targetPos;
	//float kHeroSpeed = 50.0f;
	//float attack_distance;
	//private bool ImThere = true;
	//public MYTYPE troopType;
	//bool selected = false;
	//// Use this for initialization
	//void Start () {
	//    targetPos = new Vector3(0.001f,0.001f,0.0f);
	//    switch(troopType){
	//    case MYTYPE.MELEE:
	//        attack_distance = 5.0f;
	//        break;
	//    case MYTYPE.RANGED:
	//        attack_distance = 20.0f;
	//        break;
	//    case MYTYPE.WIZARD:
	//        attack_distance = 10.0f;
	//        break;
	//    case MYTYPE.ELITE:
	//        attack_distance = 15.0f;
	//        break;
	//    case MYTYPE.BOSS:
	//        attack_distance = 15.0f;
	//        break;
	//    }
	//    attack_distance *= attack_distance;
	//}
	
	//// Update is called once per frame
	//void Update () {
	//    //if(targetPos == transform.position)
	//    //	return;

	////	float range = Vector3.SqrMagnitude(transform.position) - Vector3.SqrMagnitude(targetPos);
	////	range = Mathf.Sqrt(range);
	//    float range = 1000;



	//    if(ImThere){
	//        this.renderer.material.color = Color.red;
	//    }

	//    if(Input.GetKey(KeyCode.Alpha1)){ //melee
	//        if(troopType == MYTYPE.MELEE){
	//            selected = true;
	//            this.renderer.material.color = Color.blue;
	//        }else{
	//            selected = false;
	//        }
	//    }
	//    if(Input.GetKey(KeyCode.Alpha2)){ //ranged
	//        if(troopType == MYTYPE.RANGED){
	//            selected = true;
	//            this.renderer.material.color = Color.green;
	//        }else{
	//            selected = false;		
	//        }
	//    }
	//    if(Input.GetKey(KeyCode.Alpha3)){ //wizard
	//        if(troopType == MYTYPE.WIZARD){
	//            selected = true;
	//            this.renderer.material.color = Color.cyan;
	//        }else{
	//            selected = false;
	//        }
	//    }
	//    if(Input.GetKey(KeyCode.Alpha4)){ //elite 
	//        if(troopType == MYTYPE.ELITE){
	//            selected = true;
	//            this.renderer.material.color = Color.magenta;
	//        }
	//        else{
	//            selected = false;
	//        }
	//    }
	//    if(Input.GetKey(KeyCode.Alpha5)){ //boss
	//        if(troopType == MYTYPE.BOSS){
	//            selected = true;
	//            this.renderer.material.color = Color.yellow;
	//        }else{
	//            selected = false;	
	//        }
	//    }
	//    if(Input.GetMouseButtonDown(0)){
	//        if(selected){
	//            //transform.position = Input.mousePosition;
	//            Vector2 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	//            transform.up = tempPos;
	//            targetPos = tempPos;
	//            ImThere = false;
	//        }
	//    }
	//    if(!ImThere){
	//        transform.up = targetPos - transform.position;
	//        switch (troopType){
	//        case MYTYPE.MELEE:
	//            if(attack_distance < range){
	//                transform.position += transform.up * (kHeroSpeed * Time.smoothDeltaTime);
	//            }else{
	//                ImThere = true;
	//            }
	//            break;
	//        case MYTYPE.RANGED:
	//            if(attack_distance < range){
	//                transform.position += transform.up * (kHeroSpeed * Time.smoothDeltaTime);
	//            }else{
	//                ImThere = true;				
	//            }
	//            break;
	//        case MYTYPE.WIZARD:
	//            if(attack_distance < range){
	//                transform.position += transform.up * (kHeroSpeed * Time.smoothDeltaTime);
	//            }else{
	//                ImThere = true;
	//            }
	//            break;
	//        case MYTYPE.ELITE:
	//            if(attack_distance < range){
	//                transform.position += transform.up * (kHeroSpeed * Time.smoothDeltaTime);
	//            }else{
	//                ImThere = true;
	//            }
	//            break;
	//        case MYTYPE.BOSS:
	//            if(attack_distance < range){
	//                transform.position += transform.up * (kHeroSpeed * Time.smoothDeltaTime);
	//            }else{
	//                ImThere = true;
	//            }
	//            break;
	//        }
	//    }
	//}
	//void targetPosition(Vector3 target_pos){
	//    targetPos = target_pos;
	//    Vector3 dir = target_pos - transform.position;
	//    transform.up = dir;
	//}

	//public void setTroopType(MYTYPE c){
	//    troopType = c;
	//}
	
	////void OnMouseDown(){
	////	if(selected)
	/////		targetPos = Input.mousePosition;
	////}
}
