using UnityEngine;
using System.Collections;

public class PauseGameButton : MonoBehaviour {

	public GameObject mPauseMenuObject;
	const float kPauseInterval = 0.3f;
	float mPreviousPauseInterval = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 1) // If paused do nothing
			if(Time.realtimeSinceStartup - mPreviousPauseInterval > kPauseInterval){ // get one key press
				if(Input.GetKey(KeyCode.Space)){
					OnMouseDown();
					mPreviousPauseInterval = Time.realtimeSinceStartup;
					GameObject.Find("PauseMenuResumeButton").GetComponent<PauseMenuResumeButton>().SetPauseInterval(Time.realtimeSinceStartup);		
				}
			}		
	}
	void OnMouseDown(){
		if(Time.timeScale == 1){
			mPauseMenuObject.SetActive(true);
			Time.timeScale = 0;
		}
	}
	public void SetPauseInterval(float t){
		mPreviousPauseInterval = t;
	}
}
