using UnityEngine;
using System.Collections;

public class SpawnSizeDownButton : GTSButtonBehavior {


	void OnMouseDown(){
		if( mBonusLevel > kBonusMin && mBonusLevel > GetOriginal())  {
			mStore.mCurCost -= 15;
			mTotalGoldText.text = mStore.mCurCost.ToString();
			NewValue(-1);
		}
	}

}
