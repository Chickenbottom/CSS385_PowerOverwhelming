using UnityEngine;
using System.Collections;

public class CutSceneNextButton : ButtonBehaviour {

	public StoryBook mStoryBook;
	void OnMouseDown(){
		mStoryBook.SkipScene();
	}
}
