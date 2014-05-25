using UnityEngine;
using System.Collections;

public class ConfirmQuitButton : ButtonBehaviour {

	public DestinationBehavior destination;

	void OnMouseDown(){
		ChangeScreen();
		Time.timeScale = 1;
		string temp = destination.mDestination;
		Application.LoadLevel(destination.mDestination);
	}

}
