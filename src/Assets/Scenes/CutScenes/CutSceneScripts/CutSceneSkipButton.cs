using UnityEngine;
using System.Collections;

public class CutSceneSkipButton : ButtonBehaviour {

	public string NextLevel;
	void OnMouseDown(){
		if(NextLevel.Length == 0)
			NextLevel = "level1";
		Application.LoadLevel(NextLevel);
	}
}
