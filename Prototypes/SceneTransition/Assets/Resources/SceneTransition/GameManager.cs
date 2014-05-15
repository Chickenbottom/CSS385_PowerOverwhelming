using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnGUI()
	{
		if (GUI.Button(new Rect(100, 100, 300, 100), "Return to Level Selector"))
			Application.LoadLevel("LevelLoader");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
			Debug.Log (GameState.Score);
			
		if (Input.GetButtonDown("Fire2"))
			GameState.AddToScore(5);
	}
}
