//C#
using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	
	private const int kButtonMargin = 20;
	
	private const int kButtonHeight = 20;
	private const int kButtonWidth = 250;
	
	private static int sNumButtons = 0;
	
	private string kMenuTitle = "Power Overwhelming Prototypes";
	
	void OnGUI () 
	{
		sNumButtons = 0;
		
		CreateLevelButton("Multiple Unit Movement", "MultipleUnitMovement");
		CreateLevelButton("Another scene", "MultipleUnitMovement");
		
		// Make a background box
		GUI.Box(new Rect(10, 10, kButtonWidth + kButtonMargin, 
		                 (sNumButtons + 2) * kButtonHeight), kMenuTitle);
	}
	
	public void CreateLevelButton(string levelName, string sceneName)
	{
		sNumButtons ++;
		// Make the first button. If it is pressed, Application.Loadlevel (sceneName) will be executed
		if(GUI.Button(new Rect(20, 20 + sNumButtons * kButtonHeight, kButtonWidth, kButtonHeight), levelName)) {
			Application.LoadLevel(sceneName);
		}
		

	}
}