using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	ArrayList selectables = new ArrayList();
	Vector2 start_Mouse;
	Vector3 selection_Start;
	MeleeBehavior meleeTrooper;
	RangedBehavior rangedTrooper;
	MagicBehavior magicTrooper;
	EliteBehavior eliteTrooper;
	BossBehavior bossTrooper;
	// Use this for initialization
	void Start () {
		meleeTrooper = GameObject.Find ("Melee").GetComponent<MeleeBehavior>();
		rangedTrooper = GameObject.Find("Ranged").GetComponent<RangedBehavior>();
		magicTrooper = GameObject.Find ("Magic").GetComponent<MagicBehavior>();
		eliteTrooper = GameObject.Find ("Elite").GetComponent<EliteBehavior>();
		bossTrooper = GameObject.Find("Boss").GetComponent<BossBehavior>();
	}
	
	// Update is called once per frame
	void Update () {


		//    if(Input.GetMouseButtonDown(0)){
    //        selectionEnded = false;
    //        //Cast Ray to hit Ground
    //        Ray ray_ground = camera.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit_ground;
    //        var ground_Layermask = 1 << 8;
    //        if(Physics.Raycast(ray_ground, hit_ground, Mathf.Infinity, ground_Layermask)){
    //            selection_Start = hit_ground.point;    //Store initial hit
    //        }
    //    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    var unit_Layermask = 1 << 10;
    //    if(Physics.Raycast(ray, hit, Mathf.Infinity, unit_Layermask)){
    //        //Store Unit in Selectables
    //        selectables.Clear();
    //        hit.collider.GetComponent(Selectable).selected = true;
    //        selectables.Add(hit.collider);
    //    }
    //    //Didn't click on Ground
    //    else{
    //        selectables.Clear();
    //    }
    //    //Store the Mouse position
    //    start_Mouse = Input.mousePosition;
    //}
    //    //Multiple Unit Selection
    //    if(Input.GetMouseButton(0)){
    //        if(Vector2.Distance(start_Mouse, Input.mousePosition) > 10){    //Buffer added so it doesn't interfere wih single Selection
    //            selectionEnded = false;
    //            //Cast Ray of current mouse position on Ground
    //            var ray_Mouse: Ray = camera.ScreenPointToRay(Input.mousePosition);
    //            var hit_Mouse: RaycastHit;
    //            if(Physics.Raycast(ray_Mouse, hit_Mouse, Mathf.Infinity, 1<<8)){
    //                clearSelectables();
    //                //Set size of Selection Box
    //                transform.Find("Selection_Box").localScale = Vector3(100,1,hit_Mouse.point.z-selection_Start.z);
    //                //Set position of Selection Box to account for the size
    //                transform.Find("Selection_Box").position = Vector3(selection_Start.x, selection_Start.y, selection_Start.z+(transform.Find("Selection_Box").transform.lossyScale.z/2));
    //                //Sweep the Selection Box in the direction and get colliders
    //                var temp = transform.Find("Selection_Box").rigidbody.SweepTestAll(Vector3(hit_Mouse.point.x-selection_Start.x,0,0), Mathf.Abs(hit_Mouse.point.x-selection_Start.x));
    //                for(var l=0;l<temp.length;l++){
    //                    //Test if every collider is a Unit and on the same Team
    //                    if(temp[l].transform.GetComponent(Unit) != null && temp[l].transform.GetComponent(Selectable).team == team){
    //                        selectables.Push(temp[l].collider);
    //                        temp[l].transform.GetComponent(Selectable).selected = true;
    //                    }
    //                }
    //            }
    //        }
    //    }
		
    //    //End Selection
    //    if(Input.GetMouseButtonUp(0)){
    //        selectionEnded = true;
    //        //Hide Selection Box off screen
    //        transform.Find("Selection_Box").localScale = Vector3(1,1,1);
    //        transform.Find("Selection_Box").position = Vector3(0,0,0);
    //    }
    }
	public void unselect(){
		meleeTrooper.selected = false;
		rangedTrooper.selected = false;
		magicTrooper.selected = false;
		eliteTrooper.selected = false;
		bossTrooper.selected = false;
	}
}