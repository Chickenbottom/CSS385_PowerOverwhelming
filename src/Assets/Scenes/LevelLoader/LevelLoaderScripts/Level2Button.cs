using UnityEngine;
using System.Collections;

public class Level2Button : ButtonBehaviour {

	void OnMouseDown(){
		DifficultyLoader.mCurrentLevel = "Level2";
		DifficultyLoader.LoadGame();
	}
}
