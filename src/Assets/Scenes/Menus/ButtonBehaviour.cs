using UnityEngine;
using System.Collections;

public class ButtonBehaviour : MonoBehaviour {


	public Sprite mButton;
	public Sprite mButtonOver;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseOver(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButtonOver;
	}
	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButton;
	}
}
