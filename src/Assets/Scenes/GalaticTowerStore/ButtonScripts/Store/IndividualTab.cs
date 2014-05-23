using UnityEngine;
using System.Collections;

public class IndividualTab : MonoBehaviour {

	public SelectBar mSelectBar;
	public GameObject TabButtons;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		mSelectBar.SetTab(this.gameObject, TabButtons);
	}
}
