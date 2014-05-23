using UnityEngine;
using System.Collections;

public class ReturnToMainMenuButton : ButtonBehaviour {

	void OnMouseDown(){
		Application.LoadLevel("Menu");
	}
}
