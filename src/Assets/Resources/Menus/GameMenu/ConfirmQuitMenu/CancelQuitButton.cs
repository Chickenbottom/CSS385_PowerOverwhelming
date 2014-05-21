using UnityEngine;
using System.Collections;

public class CancelQuitButton : ButtonBehaviour {

	public GUIText mText;
	public GameObject ConfirmQuitFrame;
	// Use this for initialization
	void Start () {
		mText.text = "Are you Sure you want to quit? \nThe games progress will be lost!";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		ChangeScreen();
		ConfirmQuitFrame.SetActive(false);
		Time.timeScale = 1;
	}
}
