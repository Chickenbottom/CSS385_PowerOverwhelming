using UnityEngine;
using System.Collections;

public class SpawnSizeUpButton : GTSButtonBehavior {


	void OnMouseDown(){
		if( mBonusLevel < kBonusMax ){
			NewValue(1);
			GameObject.Find("ShaddySeamus").GetComponent<ShaddySeamusDialogue>().WritePosDialogue();	
			mStore.mCurCost += (int)UpgradeCost.SpawnSize * mBonusLevel;			
			mTotalGoldText.text = mStore.mCurCost.ToString();
		}
	}
}
