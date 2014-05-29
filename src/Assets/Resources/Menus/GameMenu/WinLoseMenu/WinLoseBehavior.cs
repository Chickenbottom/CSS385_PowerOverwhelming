using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinLoseBehavior : MonoBehaviour {

	public GameObject WinGameFrame;
	public GameObject LoseGameFrame;
	public List<GameObject> mAllFrames;

	bool TESTING = true;
	
	void Start(){
		CloseAllFrames();
	}
	// Update is called once per frame
	void Update () {
		if(GameState.WonGame)
			WinGameFrame.SetActive(true);
		if(GameState.LostGame)
			LoseGameFrame.SetActive(true);

		if(TESTING) {
			if(Input.GetKeyDown(KeyCode.W))
				GameState.WonGame = true;
			if(Input.GetKeyDown(KeyCode.L))
				GameState.LostGame = true;
		}
	}
	public void CloseAllFrames(){
		for(int i = 0; i < mAllFrames.Count; i++){
			mAllFrames[i].SetActive(false);
		}
	}
}
