using UnityEngine;
using System.Collections;

public class StoreCloseButton : ButtonBehaviour {

	void OnMouseDown(){
		Application.LoadLevel("LevelLoader");
	}
}
