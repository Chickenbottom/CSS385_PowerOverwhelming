using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnGUI()
	{
		if (GUI.Button(new Rect(120, 35, 150, 50), "Return to Level Selector"))
			Application.LoadLevel("LevelLoader");
			
		GUI.Label(new Rect(135, 65, 150, 50), "Left-Alt to spawn");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
			Debug.Log (GameState.Score);
			
		if (Input.GetButtonDown("Fire2"))
			GameState.AddToScore(5);
	}
}
