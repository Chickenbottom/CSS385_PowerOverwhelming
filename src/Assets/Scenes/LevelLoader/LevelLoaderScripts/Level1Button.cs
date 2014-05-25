using UnityEngine;
using System.Collections;

public class Level1Button : ButtonBehaviour {

	void OnMouseDown(){
		DifficultyLoader.mCurrentLevel = "Level1";
		DifficultyLoader.LoadGame();
	}
}
