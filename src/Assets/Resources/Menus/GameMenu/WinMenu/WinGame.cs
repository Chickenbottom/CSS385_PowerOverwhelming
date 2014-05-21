using UnityEngine;
using System.Collections;

public class WinGame : MonoBehaviour {

	public GameObject mWinFrame;

	void Update(){
		if(!mWinFrame.activeSelf)
			if(GameState.WonGame) 
				mWinFrame.SetActive(true);
	}

}
