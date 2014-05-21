using UnityEngine;
using System.Collections;

public class WinReplayButton : ButtonBehaviour {

	void OnButtonDown(){
		Application.LoadLevel("Level2");
	}
}
