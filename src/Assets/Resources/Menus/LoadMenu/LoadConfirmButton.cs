using UnityEngine;
using System.Collections;

public class LoadConfirmButton : ButtonBehaviour {

	public GameObject mLoadMenuFrame;
	public GameObject mNarativeContinueMenuFrame;
	void OnMouseDown(){
        SaveLoad s = GameObject.Find("SaveLoad").GetComponent<SaveLoad>();
        s.Clear(SaveLoad.SAVEFILE.Level);
        s.Load(SaveLoad.SAVEFILE.Level);
        //ChangeScreen();
        //mLoadMenuFrame.SetActive(false);
        //mNarativeContinueMenuFrame.SetActive(true);
        Debug.Log(s.GetInfo(SaveLoad.SAVEFILE.Level) + "   CHECK");
        Application.LoadLevel("LevelLoader");
	}
}
