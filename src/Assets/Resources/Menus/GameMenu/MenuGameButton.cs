using UnityEngine;
using System.Collections;

public class MenuGameButton : MonoBehaviour {

	// Use this for initialization
	const float kMenuInterval = 0.3f;
	float mPreviousMenuInterval = 0f;
	public GameObject GameMenuFrame;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 1) // If paused do nothing
		if(Time.realtimeSinceStartup - mPreviousMenuInterval > kMenuInterval){ // get one key press
			if(Input.GetKey(KeyCode.Escape)){
				OnMouseDown();
				mPreviousMenuInterval = Time.realtimeSinceStartup;
				GameObject.Find("GameMenuResumeButton").GetComponent<InGameMenuResumeButton>().SetPauseInterval(Time.realtimeSinceStartup);	
			}
		}
	}
	void OnMouseDown(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
			GameMenuFrame.SetActive(true);
		}
	}
	public void setPreviousMenuInterval(float t){
		mPreviousMenuInterval = t;
	}
}
