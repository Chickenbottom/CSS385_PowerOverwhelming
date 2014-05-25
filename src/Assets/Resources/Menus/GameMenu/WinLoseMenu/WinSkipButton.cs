using UnityEngine;
using System.Collections;

public class WinSkipButton : ButtonBehaviour {
	void OnMouseDown(){
		GameState.WonGame = GameState.LostGame = false;
		Application.LoadLevel("LevelLoader");
	}
}
