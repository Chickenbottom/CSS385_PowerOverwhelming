﻿using UnityEngine;
using System.Collections;

public class CoolDownUpButton : GTSButtonBehavior {


	void OnMouseDown(){
		if( mBonusLevel < kBonusMax ){
			NewValue(1);
			GameObject.Find("ShaddySeamus").GetComponent<ShaddySeamusDialogue>().WritePosDialogue();
			mStore.mCurCost += (int)UpgradeCost.CoolDown * mBonusLevel;
			mTotalGoldText.text = mStore.mCurCost.ToString();
		}
	}
}
