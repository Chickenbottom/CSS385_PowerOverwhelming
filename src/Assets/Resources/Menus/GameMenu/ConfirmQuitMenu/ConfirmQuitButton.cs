using UnityEngine;
using System.Collections;

public class ConfirmQuitButton : ButtonBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnMouseDown(){
		ChangeScreen();
		Time.timeScale = 1;
		Application.LoadLevel("LevelLoader");
	}

}
