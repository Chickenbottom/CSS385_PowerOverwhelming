using UnityEngine;
using System.Collections;

public class CutSceneSkipButton : ButtonBehaviour {

	public StoryBook mStoryBook;
	void OnMouseDown(){
        if(mStoryBook.GetMyAction() != Action.ChangingSet || 
           mStoryBook.GetMyAction() != Action.FadeIn)
		    mStoryBook.QuitNarative();
	}
}
