using UnityEngine;
using System.Collections;

public class SecondaryButtonBehaviour : MonoBehaviour {
	
	public enum Destination{
		MainMenu,
		GTS,
	};
	public Sprite mButtonDown;
	public Sprite mButton;
	public Destination mDestination;

	bool mLevelLocked;
	
	// Use this for initialization
	void Start () {			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseOver(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButtonDown;
	}
	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = mButton;
	}
	void OnMouseDown(){
			switch(mDestination){
			case Destination.GTS:
				Application.LoadLevel("TowerStore");
				break;
			case Destination.MainMenu:
				Application.LoadLevel("Menu");
				break;
			}
		}

}
