using UnityEngine;
using System.Collections;

public class GTSButton : MonoBehaviour {

    public Sprite mButton;
    public Sprite mButtonOver;
    public GUIText mDisplay;
    public string mDisplayText;


    void OnMouseOver(){
        mDisplay.text = mDisplayText;
        gameObject.GetComponent<SpriteRenderer> ().sprite = mButtonOver;
    }
    void OnMouseExit(){
        mDisplay.text = "";
        gameObject.GetComponent<SpriteRenderer> ().sprite = mButton;
    }
    void OnMouseDown(){
		Application.LoadLevel("TowerStore");
	}
}
