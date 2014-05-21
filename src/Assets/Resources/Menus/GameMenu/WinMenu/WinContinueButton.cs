using UnityEngine;
using System.Collections;

public class WinContinueButton : ButtonBehaviour {

	void OnMouseDown(){
		Application.LoadLevel("LevelLoader");
	}
}
