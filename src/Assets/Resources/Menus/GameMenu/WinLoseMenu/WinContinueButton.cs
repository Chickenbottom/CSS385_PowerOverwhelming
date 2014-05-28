using UnityEngine;
using System.Collections;
using UnityEditor;

public class WinContinueButton : ButtonBehaviour {

	public enum NextStep{
		NextLevel,
		CutScene,
	};

	public NextStep mNextStep;

	void OnMouseDown(){
		string[] path = EditorApplication.currentScene.Split(char.Parse("/"));
		string[] pathEnd = path[path.Length-1].Split(char.Parse("."));
		string[] level = pathEnd[0].Split(char.Parse("_"));

		int levelNum = int.Parse(level[0]);
		GameState.WonGame = GameState.LostGame = false;

		switch(mNextStep){
		case NextStep.NextLevel:
			Application.LoadLevel(pathEnd[0] + (levelNum+1).ToString());
			break;
		case NextStep.CutScene:
			Application.LoadLevel(pathEnd[0] + "_CutScene");
			break;
		}
	}
}
