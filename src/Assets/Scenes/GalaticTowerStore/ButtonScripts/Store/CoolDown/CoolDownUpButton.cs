using UnityEngine;
using System.Collections;

public class CoolDownUpButton : GTSButtonBehavior {

	void OnMouseDown(){
		if( mBonusLevel < kBonusMax ){
			mStore.mCurCost += 20;
			mTotalGoldText.text = mStore.mCurCost.ToString();
			NewValue(1);
		}
	}
}
