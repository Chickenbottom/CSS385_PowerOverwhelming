using UnityEngine;
using System.Collections;

public class DifficultyLoaderButtonBehavior : LevelLoaderButtonBehavior {

	public string mLevel;

	void OnMouseDown(){
		switch(GetEra()){
		case Era.Prehistoric:
			Application.LoadLevel("TowerStore");
			break;
		case Era.Medieval:
			Application.LoadLevel(mLevel);
			break;
		case Era.Japanese:
			Application.LoadLevel("TowerStore");
			break;
		case Era.ModernAmerica:
			Application.LoadLevel("TowerStore");
			break;
		case Era.Future:
			Application.LoadLevel("TowerStore");
			break;
		}
	}
}
