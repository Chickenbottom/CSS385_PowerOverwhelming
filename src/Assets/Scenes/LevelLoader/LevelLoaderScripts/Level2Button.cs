using UnityEngine;
using System.Collections;

public class Level2Button : ButtonBehaviour {

	void OnMouseDown(){
		DifficultyLoader.mCurrentLevel = "Level.2";
		DifficultyLoader.LoadGame();
	}
}
