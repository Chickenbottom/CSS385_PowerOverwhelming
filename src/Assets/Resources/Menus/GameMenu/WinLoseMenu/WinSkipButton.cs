using UnityEngine;
using System.Collections;
using System;

public class WinSkipButton : ButtonBehaviour {
	public string mNextStep;
	void OnMouseDown(){

		GameState.WonGame = GameState.LostGame = false;
		try{
		Application.LoadLevel(mNextStep);
		}
		catch(Exception e){
			Application.LoadLevel("LevelLoader");
		}
	}
}
