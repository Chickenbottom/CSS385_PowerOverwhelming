using UnityEngine;
using System.Collections;

public class StoreCloseButton : ButtonBehaviour {

	void OnMouseDown(){
        SaveLoad s = GameObject.Find("SaveLoad").GetComponent<SaveLoad>();
        s.Clear(SaveLoad.SAVEFILE.Level);
        s.Load(SaveLoad.SAVEFILE.Level);
        string level = s.GetInfo(SaveLoad.SAVEFILE.Level)[1];
        s.Clear(SaveLoad.SAVEFILE.Level);
        s.Add(level, SaveLoad.SAVEFILE.Level);
        s.Add("" + GameState.Gold, SaveLoad.SAVEFILE.Level);
        s.Save();
		Application.LoadLevel("LevelLoader");
	}
}
