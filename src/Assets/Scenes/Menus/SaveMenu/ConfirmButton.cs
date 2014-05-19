using UnityEngine;
using System.Collections;

public class ConfirmButton : ButtonBehaviour {
	void OnMouseDown()
	{ 
		Application.LoadLevel("Medieval0");	
	}
}
