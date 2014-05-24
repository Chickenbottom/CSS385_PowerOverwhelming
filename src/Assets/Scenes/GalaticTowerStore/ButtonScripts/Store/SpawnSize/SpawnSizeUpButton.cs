using UnityEngine;
using System.Collections;

public class SpawnSizeUpButton : GTSButtonBehavior {

	void OnMouseDown(){
		if( mBonusLevel < kBonusMax ){
			mStore.mCurCost += 15;
			mTotalGoldText.text = mStore.mCurCost.ToString();
			NewValue(1);
		}
	}
}
