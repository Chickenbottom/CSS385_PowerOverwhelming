using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	//Selected
	bool selected = false;
	
	//Team and Alliance
	int team = 0;    //Team is the player who the unit is loyal to
	int alliance = 0;//Alliance is the group of players it is loyal to
	
	//Color depends on the team that the player is on
	private Color[] color_ID = new Color[3];
	
	//Set the color of the unit to the team color
	void Start(){
		color_ID[0] = Color.white;
		color_ID[1] = Color.green;
		color_ID[2] = Color.red;
		renderer.material.color = color_ID[team];
	}
	
	//If a unit is selected then enable its Projector to show it
	void Update(){
//		if(selected){
//			if(!transform.Find("SelectedPlane").Find("Plane").GetComponent(MeshRenderer).enabled)
//				transform.Find("SelectedPlane").Find("Plane").GetComponent(MeshRenderer).enabled = true;
//			if(transform.Find("MinimapBounds").renderer.material.color != color_ID[team]+Color(.2,.2,.2))
//				transform.Find("MinimapBounds").renderer.material.color = color_ID[team]+Color(.2,.2,.2);
//		}
//		else{
//			if(transform.Find("SelectedPlane").Find("Plane").GetComponent(MeshRenderer).enabled)
//				transform.Find("SelectedPlane").Find("Plane").GetComponent(MeshRenderer).enabled = false;
//			if(transform.Find("MinimapBounds").renderer.material.color != color_ID[team])
//				transform.Find("MinimapBounds").renderer.material.color = color_ID[team];
//		}
//	}
	}
}
