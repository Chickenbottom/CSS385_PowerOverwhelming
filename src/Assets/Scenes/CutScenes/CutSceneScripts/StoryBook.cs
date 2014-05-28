using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StoryBook : MonoBehaviour {


	enum Action{
		FadeIn,
		FadeOut,
		Printing,
		ChangingSet,
		Finished,
		Waiting,
	};

	public string NextScene;
    public List<SpriteRenderer> mImageArray;
	public string FilePath;
	public GUIText mDialogueText;

	Action mMyAction;
	int mCurrentImageIndex = 0;
	int mDialogueLetterIndex = 0;	
	private SpriteRenderer mCurrentImage;
	private ArrayList mDialogueArray = new ArrayList();
	StreamReader mFile;
	float alpha;
	float mPreviousLetterDisplayTime = 0f;
	string mCurrentDialogueString = null;
	bool mDialogueExists = true;
	bool mMultipleLines = false;

    const float kFadeRate = 0.01f;
	const float kLetterDisplayDelay = 0.03f;	
	float kSceneTransitionWaitTime = 1.5f;

	// Use this for initialization
	void Start () {
		if(File.Exists(FilePath))
			LoadDialogue();
		else{
			Debug.LogError ("Could not find Dialogue text file");
			mDialogueExists = false;
			kSceneTransitionWaitTime += 2f;
		}

		for (int i = 0; i < mImageArray.Count; i++) {
			setAlphaToZero(mImageArray[i]);
		}

		mMyAction = Action.FadeIn;
		mDialogueText.text = "";
		mCurrentDialogueString = mDialogueArray[mCurrentImageIndex].ToString();
		mCurrentImage = mImageArray[0];
	}
	
	// Update is called once per frame
	void Update () {
		switch(mMyAction){
		case Action.FadeIn:
			alpha = setAlpha ();
			alpha += kFadeRate;
			setImageColor (alpha);
			if (alpha >= 1.0f)
				mMyAction = Action.Printing;
			break;
		case Action.FadeOut:
			alpha = setAlpha ();
			alpha -= kFadeRate;
			setImageColor (alpha);
			if (alpha <= 0.0f)
				mMyAction = Action.ChangingSet;
			break;
		case Action.Printing:
			if(!mDialogueExists)
				mMyAction = Action.Waiting;

			if(mCurrentDialogueString.Length > 73)
				if(mDialogueLetterIndex % 73 == 0 && !mCurrentDialogueString.Substring(0,73).Contains("\n") &&
				   mDialogueLetterIndex != 0)
					mDialogueText.text += "\n";
			if(mDialogueLetterIndex < mCurrentDialogueString.Length){
				if(Time.time - mPreviousLetterDisplayTime > kLetterDisplayDelay){
					mDialogueText.text += mCurrentDialogueString[mDialogueLetterIndex].ToString();
					mDialogueLetterIndex++;
				}
			}
			else{
				mMyAction = Action.Waiting;
				mDialogueLetterIndex = 0;
			}
			break;
		case Action.ChangingSet:
			if(mCurrentImageIndex < mImageArray.Count && mCurrentImageIndex >= 0)
				setAlphaToZero(mImageArray[mCurrentImageIndex]);

			mCurrentImageIndex++;

			if(mCurrentImageIndex < mImageArray.Count){

				mCurrentDialogueString = mDialogueArray[mCurrentImageIndex].ToString();
				mCurrentImage = mImageArray[mCurrentImageIndex];
				mMyAction = Action.FadeIn;
			}
			else
				mMyAction = Action.Finished;
			break;
		case Action.Finished:
			Application.LoadLevel(NextScene);
			break;
		case Action.Waiting:
			Invoke("FrameWaited",kSceneTransitionWaitTime);
			break;
		}
	}
	void FrameWaited(){
		mDialogueText.text = "";
		mMyAction = Action.FadeOut;
	}
	void LoadDialogue ()
	{
		mFile = new StreamReader(FilePath);	
		while(!mFile.EndOfStream){
			string FullLine = "";
			string line = mFile.ReadLine();
			string[] Speakers = line.Split(char.Parse(">"));
			foreach(string saying in Speakers)
				FullLine += saying + "\n";

			mDialogueArray.Add(FullLine);
		}
	}
	void setAlphaToZero(SpriteRenderer CurImage){
		CurImage.material.color = new Color (CurImage.material.color.r, 
		                                               CurImage.material.color.g,
		                                               CurImage.material.color.b, 
		                                         		0);
		
	}
	void setImageColor (float alpha)
	{
		mCurrentImage.material.color = new Color (mCurrentImage.material.color.r, 
		                                          mCurrentImage.material.color.g,
		                                          mCurrentImage.material.color.b, 
		                                          alpha);
	}
	 float setAlpha ()
	{
		if(mCurrentImage != null)
			return mCurrentImage.material.color.a;
		else 
			return 1f;
	}
	public void NextFrame(){
		if (mCurrentImageIndex + 1 >= mImageArray.Count || mMyAction == Action.FadeIn ||
		    mMyAction == Action.ChangingSet || mMyAction == Action.Finished || mMyAction == Action.FadeOut){
			return;
		}
		else{
			mDialogueText.text = "";
			mDialogueLetterIndex = 0;
			mMyAction = Action.ChangingSet;
		}
	}
	public void PreviousFrame(){
		if(mCurrentImageIndex -1 < 0)
			return;

		setAlphaToZero(mImageArray[mCurrentImageIndex]);
		mDialogueText.text = "";
		mDialogueLetterIndex = 0;		
		mCurrentImageIndex -= 2;

		mMyAction = Action.ChangingSet;
	}
	public int GetCurrentImage(){
		if(mCurrentImageIndex + 1 <= mImageArray.Count)
			return mCurrentImageIndex + 1;
		else
			return mImageArray.Count;
	}
	public int GetTotalImages(){
		return mImageArray.Count;
	}
	public void QuitNarative(){
		mMyAction = Action.Finished;	
	}
}
