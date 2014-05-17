using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryBook : MonoBehaviour
{
    public List<Camera> CameraViews;
    public SpriteRenderer Image;
    public float SceneInterval = 3f;
    
    const float kFadeRate = 0.01f;
    
    private float mCamInterval = 3f;
    private List<Camera> mCameraArray = new List<Camera> ();
    
    private float mPreviousCam = 0f;
    private int mIndex = 1;
    private bool mFadeIn = false;
    
    void Start ()
    {
        if (CameraViews == null || CameraViews.Count == 0)
            Debug.LogError ("Cameras need to be added to this script in the Unity inspector");
        
        mCamInterval = SceneInterval;

        mCameraArray = CameraViews;
        mCameraArray [0].enabled = true;
        for (int i = 1; i < mCameraArray.Count; i++) {
            mCameraArray [i].enabled = false;
        }
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (mFadeIn)
            FadeIn ();
        if (mIndex <= mCameraArray.Count)
        if (Time.realtimeSinceStartup - mPreviousCam > mCamInterval && mIndex < mCameraArray.Count) {
            if (FadeOut ()) {
                Camera temp = (Camera)mCameraArray [mIndex++];
                temp.camera.enabled = true;
                mPreviousCam = Time.realtimeSinceStartup;
                mFadeIn = true;
            }
        }
    }

    bool FadeIn ()
    {
        float alpha = setAlpha ();
        alpha += kFadeRate;
        setImageColor (alpha);
        if (alpha >= 1.0f) {
            mFadeIn = false;
            return true;
        } else
            return false;

    }

    bool FadeOut ()
    {
        float alpha = setAlpha ();
        alpha -= kFadeRate;
        setImageColor (alpha);
        if (alpha <= 0.0f)
            return true;
        else
            return false;
    }

    float setAlpha ()
    {
        return Image.material.color.a;
    }

    void setImageColor (float alpha)
    {
        Image.material.color = new Color (Image.material.color.r, Image.material.color.g,
                                         Image.material.color.b, alpha);
	}
	void StartGame(){
		Application.LoadLevel("Level2");
	}
}
