using UnityEngine;
using System.Collections;

public class PauseMenuResumeButton : ButtonBehaviour {

	public GameObject mPauseMenuFrame;
	// Use this for initialization
	const float kPauseInterval = 0.3f;
	float mPreviousPauseInterval = 0f; 
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(mPauseMenuFrame.activeSelf){
			if(Time.realtimeSinceStartup - mPreviousPauseInterval > kPauseInterval){
				if(Input.GetKey(KeyCode.Space)){
					mPreviousPauseInterval = Time.realtimeSinceStartup;
					GameObject.Find("PauseGameButton").GetComponent<PauseGameButton>().SetPauseInterval(Time.realtimeSinceStartup);
					OnMouseDown();
				}
			}
		}
	}
	void OnMouseDown(){
		ChangeScreen();
		mPauseMenuFrame.SetActive(false);
		Time.timeScale = 1;
	}
	public void SetPauseInterval(float t){
		mPreviousPauseInterval = t;
	}
}
