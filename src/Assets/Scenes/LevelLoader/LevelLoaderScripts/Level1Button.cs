using UnityEngine;
using System.Collections;
using System;

public class Level1Button : ButtonBehaviour {

	void OnMouseDown(){
		DifficultyLoader.mCurrentLevel = Enum.GetName(typeof(EraLevel), EraLevel._1);
		DifficultyLoader.LoadGame();
	}
}
