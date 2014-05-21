using UnityEngine;
using System.Collections;

public class WinMenuButton : ButtonBehaviour {
	void OnButtonDown(){
		Application.LoadLevel("Menu");
	}
}
