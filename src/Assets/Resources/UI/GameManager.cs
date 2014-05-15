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
	
	Dictionary<UnitType, ProgressBar> expBars;
	
	void Start()
	{
		expBars = new Dictionary<UnitType, ProgressBar>();
		expBars.Add(UnitType.Archer, GameObject.Find ("ArcherExperience").GetComponent<ProgressBar>());
		expBars.Add(UnitType.Swordsman, GameObject.Find ("SwordsmanExperience").GetComponent<ProgressBar>());
		expBars.Add(UnitType.Mage, GameObject.Find ("MageExperience").GetComponent<ProgressBar>());
	}
	
	void Update()
	{
		foreach(UnitType u in expBars.Keys) {
			expBars[u].UpdateValue(GameState.UnitExperience[u]);
		}
	}
}
