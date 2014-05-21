using UnityEngine;
using System.Collections;

public class TotalSceneText : MonoBehaviour {

	public StoryBook mStoreBook;
	public GUIText mText;
	// Use this for initialization
	void Start () {
		mText.text = mStoreBook.CameraViews.Count.ToString();
	}
}
