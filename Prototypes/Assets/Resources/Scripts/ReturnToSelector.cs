using UnityEngine;
using System.Collections;

public class ReturnToSelector : MonoBehaviour {

	private const int kButtonMargin = 20;
	private const int kButtonWidth = 250;
	
	private string kMenuTitle = "Menu";
	
	void OnGUI () 
	{
		// Make a background box1
		GUI.Box(new Rect(10, 10, kButtonWidth + kButtonMargin, 90), kMenuTitle);
		if(GUI.Button(new Rect(20, 40, kButtonWidth, 20), "Return to Level Selector")) {
			Application.LoadLevel("LevelSelector");
		}
		
	}
}
