using UnityEngine;
using System.Collections;

public class LoadConfirmButton : ButtonBehaviour {

	void OnMouseDown(){
		ChangeScreen();
		Application.LoadLevel("LevelLoader");
	}
}
