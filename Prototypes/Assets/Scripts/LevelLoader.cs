//C#
using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	
	private const int kButtonMargin = 20;
	private const int kButtonWidth = 250;
	
	private string kMenuTitle = "Power Overwhelming Prototypes";
	
	void OnGUI () 
	{
		// Make a background box
		GUI.Box(new Rect(10, 10, kButtonWidth + kButtonMargin, 90), kMenuTitle);
		
		CreateLevelButton("Multiple Unit Movement", "MultipleUnitMovement");
	}
	
	public void CreateLevelButton(string levelName, string sceneName)
	{
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20, 40, kButtonWidth, 20), levelName)) {
			Application.LoadLevel(sceneName);
		}
	
	}
}