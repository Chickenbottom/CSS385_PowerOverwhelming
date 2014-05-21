using UnityEngine;
using System.Collections;

public class InGameMenuResumeButton : ButtonBehaviour {

	float mPreviousPauseInterval = 0;
	const float kPauseInterval = 0.3f;
	public GameObject GameMenuFrame;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameMenuFrame.activeSelf){
			if(Time.realtimeSinceStartup - mPreviousPauseInterval > kPauseInterval){
				if(Input.GetKey(KeyCode.Escape)){
					mPreviousPauseInterval = Time.realtimeSinceStartup;
					GameObject.Find("MenuGameButton").GetComponent<MenuGameButton>().setPreviousMenuInterval(Time.realtimeSinceStartup);
					OnMouseDown();
				}
			}
		}

	}
	public void SetPauseInterval(float t){
		mPreviousPauseInterval = t;
	}
	void OnMouseDown(){
		Time.timeScale = 1;
		GameMenuFrame.SetActive(false);
	}
}
