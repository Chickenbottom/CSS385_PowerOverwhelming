using UnityEngine;
using System.Collections;
using System;

public class Level2Button : ButtonBehaviour {

	void OnMouseDown(){
		DifficultyLoader.mCurrentLevel = Enum.GetName(typeof(EraLevel), EraLevel._2);
		DifficultyLoader.LoadGame();
	}
}
