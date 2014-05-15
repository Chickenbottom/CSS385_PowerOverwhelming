using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public void OnGUI()
	{
		if (GUI.Button(new Rect(120, 35, 150, 50), "Return to Level Selector"))
			Application.LoadLevel("LevelLoader");
			
		GUI.Label(new Rect(135, 65, 150, 50), "Left-Alt to spawn");
	}
	
	Dictionary<UnitType, Progressbar> mExpBars;
	
	void Start()
	{
		mExpBars = new Dictionary<UnitType, Progressbar>();
		mExpBars.Add(UnitType.kArcher, GameObject.Find ("ArcherExperience").GetComponent<Progressbar>());
		mExpBars.Add(UnitType.kSwordsman, GameObject.Find ("SwordsmanExperience").GetComponent<Progressbar>());
		mExpBars.Add(UnitType.kMage, GameObject.Find ("MageExperience").GetComponent<Progressbar>());
	}
	
	void Update()
	{
		foreach(UnitType u in mExpBars.Keys) {
			mExpBars[u].UpdateValue(GameState.UnitExperience[u]);
		}
	}
}
