using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MainMenuBehaviour : MonoBehaviour {

	public List<GameObject> mList;
	// Use this for initialization
	void Start () {

		for(int i = 0; i < mList.Count; i++){
			if(mList[i].name == "MenuFrame")
				mList[i].SetActive(true);
			else
				mList[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
