using UnityEngine;
using System.Collections;

public class TowerHealthUpButton : TowerHeal {

	const int kBonusMax = 5;
	void OnMouseDown(){
		if( mBonusLevel < kBonusMax ){
			mStore.mCurCost += 10;
			mTotalGoldText.text = mStore.mCurCost.ToString();
			NewValue(1);
		}
	}
}
