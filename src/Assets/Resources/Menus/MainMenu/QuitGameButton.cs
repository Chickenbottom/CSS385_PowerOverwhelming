using UnityEngine;
using System.Collections;

public class QuitGameButton : MonoBehaviour {

	void OnMouseDown(){
		Application.Quit();
	}
}
