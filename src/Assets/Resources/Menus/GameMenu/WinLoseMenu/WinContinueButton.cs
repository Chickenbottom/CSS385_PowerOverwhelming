using UnityEngine;
using System.Collections;

public class WinContinueButton : ButtonBehaviour {

	void OnMouseDown(){
		GameState.WonGame = GameState.LostGame = false;
		Application.LoadLevel("LevelLoader");
	}
}
