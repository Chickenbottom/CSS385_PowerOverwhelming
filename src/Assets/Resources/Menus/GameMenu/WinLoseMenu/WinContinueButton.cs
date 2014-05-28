using UnityEngine;
using System.Collections;
using UnityEditor;

public class WinContinueButton : ButtonBehaviour {



	public string mNextStep;

	void OnMouseDown(){
	//  a more generalized form to be used with multple levels 
	//	string[] path = EditorApplication.currentScene.Split(char.Parse("/"));
	//	string[] pathEnd = path[path.Length-1].Split(char.Parse("."));
	//	string[] level = pathEnd[0].Split(char.Parse("_"));

	//	int levelNum = int.Parse(level[0]);
		GameState.WonGame = GameState.LostGame = false;

		Application.LoadLevel(mNextStep);
	}
}
