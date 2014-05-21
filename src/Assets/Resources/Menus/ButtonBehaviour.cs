using UnityEngine;
using System.Collections;

public class ButtonBehaviour : MonoBehaviour {


	public Sprite mButton;
	public Sprite mButtonOver;
	private static bool mMouseOver = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(!mMouseOver)
			gameObject.GetComponent<SpriteRenderer> ().sprite = mButton;
	}
	void OnMouseOver(){
		mMouseOver = true;
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButtonOver;
	}
	void OnMouseExit(){
		mMouseOver = false;
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButton;
	}
	public void ChangeScreen(){
		mMouseOver = false;
	}
}
