using UnityEngine;
using System.Collections;
//using UnityEditor;

public class WinLoseReplayButton : ButtonBehaviour {
	void OnMouseDown(){
		//string[] path = UnityEditor.EditorApplication.currentScene.Split(char.Parse("/"));
		//string[] pathEnd = path[path.Length-1].Split(char.Parse("."));


		GameState.WonGame = GameState.LostGame = false;
		Application.LoadLevel("LevelLoader");
	}
}
