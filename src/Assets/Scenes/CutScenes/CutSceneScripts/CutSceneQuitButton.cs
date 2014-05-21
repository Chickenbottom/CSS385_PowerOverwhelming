using UnityEngine;
using System.Collections;

public class CutSceneQuitButton : ButtonBehaviour {

	void OnButtonDown(){
		Application.LoadLevel("Menu");
	}
}
