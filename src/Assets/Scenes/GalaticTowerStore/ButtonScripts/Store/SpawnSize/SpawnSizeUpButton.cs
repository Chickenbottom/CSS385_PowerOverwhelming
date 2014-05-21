using UnityEngine;
using System.Collections;

public class SpawnSizeUpButton : SpawnSize {

	const int kBonusMax = 5;
	void OnMouseDown(){
		if( (BonusLevel+1) <= kBonusMax ){
			int temp = int.Parse(mTotalGold.text);
			temp += 15;
			mTotalGold.text = temp.ToString();
			NewValue(1);
	}
	}
}
