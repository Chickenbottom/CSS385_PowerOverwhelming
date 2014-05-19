using UnityEngine;
using System.Collections;

public class MainMenuBehaviour : MonoBehaviour {

	public GameObject mAboutFrame;
	public GameObject mLoadFrame;
	public GameObject mMenuFrame;
	public GameObject mOverwriteFrame;
	public GameObject mSaveFrame;
	// Use this for initialization
	void Start () {
		mMenuFrame.SetActive(true);
		mLoadFrame.SetActive(false);
		mAboutFrame.SetActive(false);
		mOverwriteFrame.SetActive(false);
		mSaveFrame.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
