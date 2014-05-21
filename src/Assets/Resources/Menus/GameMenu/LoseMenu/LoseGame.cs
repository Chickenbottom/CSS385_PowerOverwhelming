using UnityEngine;
using System.Collections;

public class LoseGame : MonoBehaviour {

	public GameObject mLoseFrame;

	// Update is called once per frame
	void Update () {
		if(!mLoseFrame.activeSelf)
			if(GameState.LostGame){
				mLoseFrame.SetActive(true);
	//		Time.timeScale = 0;
		}
	}
}
