using UnityEngine;
using System.Collections;

public class AboutMenuText : MonoBehaviour {

	public GUIText mText;
	
	// Use this for initialization
	void Start () {
		mText.text = "Developers\t\t\t\t\n";
		mText.text += "James Murphree \nRodelle Ladia\nChad Hickenbottom \n";
		mText.text += "Artist\t\t\t\t\t\n";
		mText.text += "Angela Liu\n";
		mText.text += "Music\t\t\t\t\t\n";
		mText.text += "Jd Awald"; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
