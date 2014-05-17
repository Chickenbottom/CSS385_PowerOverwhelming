using UnityEngine;
using System.Collections;

public class StoryBook : MonoBehaviour {

	public Camera mCamera0;
	public Camera mCamera1;
	public Camera mCamera2;
	public Camera mCamera3;
	public Camera mCamera4;

	public SpriteRenderer Image;

	const float kCamInterval = 10f;
	const float kFadeRate = 0.01f;

	ArrayList mCameraArray = new ArrayList();

	float alpha = 0.0f;
	float mPreviousCam = 0f;
	int mIndex = 1;

	bool mFadeIn = false;
	bool mFadeOut = false;
	// Use this for initialization
	void Start () {
		mCameraArray.Add(mCamera0);
		mCameraArray.Add(mCamera1);
		mCameraArray.Add(mCamera2);
		mCameraArray.Add(mCamera3);
		mCameraArray.Add(mCamera4);

		mCamera0.camera.enabled = true;
		for(int i = 1; i < mCameraArray.Count; i++){
			Camera temp = (Camera) mCameraArray[i];
			temp.camera.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(mFadeIn)
			FadeIn();
		if(mIndex <= mCameraArray.Count)
			if(Time.realtimeSinceStartup - mPreviousCam > kCamInterval && mIndex < mCameraArray.Count){
				if(FadeOut()){
					Camera temp = (Camera) mCameraArray[mIndex++];
							temp.camera.enabled = true;
					mPreviousCam = Time.realtimeSinceStartup;
					mFadeIn = true;
				}
		}
	}
	bool FadeIn(){
		float alpha = setAlpha();
		alpha += kFadeRate;
		setImageColor(alpha);
		if(alpha >= 1.0f){
			mFadeIn = false;
			return true;
		}
		else
			return false;

	}

	bool FadeOut(){
		float alpha = setAlpha();
		alpha -= kFadeRate;
		setImageColor(alpha);
		if(alpha <= 0.0f)
			return true;
		else
			return false;
	}

	float setAlpha(){
		return Image.material.color.a;
	}
	void setImageColor(float alpha){
		Image.material.color = new Color(Image.material.color.r,Image.material.color.g,
		                                 Image.material.color.b, alpha);

	}
}
