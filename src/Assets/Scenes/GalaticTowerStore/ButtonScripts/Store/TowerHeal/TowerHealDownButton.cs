using UnityEngine;
using System.Collections;

public class TowerHealDownButton : GTSButtonBehavior {

	void OnMouseDown(){
		if( mBonusLevel > kBonusMin && mBonusLevel > GetOriginal()) {
			mStore.mCurCost -= 10;
			mTotalGoldText.text = mStore.mCurCost.ToString();
			NewValue(-1);
		}
	}
}
