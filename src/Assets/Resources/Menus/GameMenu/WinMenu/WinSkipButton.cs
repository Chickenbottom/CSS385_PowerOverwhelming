using UnityEngine;
using System.Collections;

public class WinSkipButton : ButtonBehaviour {
	void OnMouseDown(){
		Application.LoadLevel("Level3");
	}
}
