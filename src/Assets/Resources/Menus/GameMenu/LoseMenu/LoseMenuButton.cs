using UnityEngine;
using System.Collections;

public class LoseMenuButton : ButtonBehaviour {
	void OnMouseDown(){
		Application.LoadLevel("Menu");
	}
}
