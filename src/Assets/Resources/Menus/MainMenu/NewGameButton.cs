using UnityEngine;
using System.Collections;
using System.IO;
public class NewGameButton : ButtonBehaviour {
	//public GameObject mSaveMenuObject; //uncomment after save load is working

	//remove after save / load is working
	public GameObject mNarativeContinueFrame;
	public GameObject mMenuObject;
    string file = "Bonus.txt";
	void OnMouseDown(){

		//after save / load game is up and running
		//mSaveMenuObject.SetActive(true);
        if(File.Exists(file))
            File.Delete(file);

        UnitStats.ResetLevels();
        SaveLoad s = GameObject.Find("SaveLoad").GetComponent<SaveLoad>();
        s.Clear(SaveLoad.SAVEFILE.Level);
        s.Add("1", SaveLoad.SAVEFILE.Level);
        s.Add("0", SaveLoad.SAVEFILE.Level);
        s.Save();


		Application.LoadLevel("TutorialCutScene");
		//remove after save load is working
		//mNarativeContinueFrame.SetActive(true);
		//mMenuObject.SetActive(false);
		//ChangeScreen();
		
	}

}

