using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

	enum ELEGANCE{
		RODELLE,
		PEASANT,
	};

	ELEGANCE myElegance;
	int hit_points;
	// Use this for initialization
	void Start () {
		hit_points = 100;
		myElegance = ELEGANCE.RODELLE;
	}
	
	// Update is called once per frame
	void Update () {
		if(hit_points <= 0){
			if(myElegance == ELEGANCE.RODELLE)
			   myElegance = ELEGANCE.PEASANT;
		   else
				myElegance = ELEGANCE.RODELLE;
	   }
	}
	void OnMouseDown(){

	}
	void hitTower(int hp){
		hit_points -= hp;
	}
}
