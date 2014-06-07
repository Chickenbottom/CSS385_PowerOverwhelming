using UnityEngine;
using System.Collections;

public class LoadButton : ButtonBehaviour {


	public GameObject mLoadFrameObject;
	public GameObject mMenuFrameObject;
    private bool isActive;

	// Use this for initialization
	void Start () {
        isActive = true;
	}

	void OnMouseDown(){
        //SaveLoad s = GameObject.Find("SaveLoad").GetComponent<SaveLoad>();
		
        //s.Clear(SaveLoad.SAVEFILE.Level);
        //s.Load(SaveLoad.SAVEFILE.Level);
        //ChangeScreen();
		//mLoadMenuFrame.SetActive(false);
		//mNarativeContinueMenuFrame.SetActive(true);
        if (isActive)
        {
            Application.LoadLevel("LevelLoader"); 
        }
        
		//mLoadFrameObject.SetActive(true);
		//mMenuFrameObject.SetActive(false);
		//ChangeScreen();
	}

    public void setInactive()
    {
        isActive = false;
        // set sprite to greyed out
    }
}
