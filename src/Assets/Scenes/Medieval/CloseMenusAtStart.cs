using UnityEngine;
using System.Collections;

public class CloseMenusAtStart : MonoBehaviour {

	public GameObject PauseMenuFrame;
	public GameObject ConfirmQuitFrame;
	public GameObject OptionsMenuFrame;
	// Use this for initialization
	void Start () {
		PauseMenuFrame.SetActive(false);
		ConfirmQuitFrame.SetActive(false);
		OptionsMenuFrame.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
