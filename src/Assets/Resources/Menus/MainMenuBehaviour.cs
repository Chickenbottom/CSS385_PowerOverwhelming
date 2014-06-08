﻿using UnityEngine;
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
        SaveLoad s = GameObject.Find("SaveLoad").GetComponent<SaveLoad>();
        s.Clear(SaveLoad.SAVEFILE.Level);
        s.Load(SaveLoad.SAVEFILE.Level);
        if (!s.LoadSuccessful())
        {
            GameObject.Find("LoadButton").GetComponent<LoadButton>().setInactive();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
