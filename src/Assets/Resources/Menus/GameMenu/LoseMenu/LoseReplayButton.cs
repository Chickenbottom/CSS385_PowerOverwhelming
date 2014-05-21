using UnityEngine;
using System.Collections;

public class LoseReplayButton : ButtonBehaviour {
	void OnMouseDonw(){
		Application.LoadLevel("Level2");
	}
}
