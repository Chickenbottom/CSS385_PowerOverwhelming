using UnityEngine;
using System.Collections;

public class CoolDownDownButton : CoolDown {


	
	void OnMouseDown(){
		if( mBonusLevel > kBonusMin && mBonusLevel > GetOriginal()){
			mStore.mCurCost -= 20;
			mTotalGoldText.text = mStore.mCurCost.ToString();
			NewValue(-1);
		}
	}
}
