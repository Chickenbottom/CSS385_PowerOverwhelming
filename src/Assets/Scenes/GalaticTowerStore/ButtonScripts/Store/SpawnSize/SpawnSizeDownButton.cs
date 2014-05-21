using UnityEngine;
using System.Collections;

public class SpawnSizeDownButton : SpawnSize {


	void OnMouseDown(){
		if( ( BonusLevel - 1) > 0 || ( BonusLevel - 1 ) > GetOriginal())  {
			int temp = int.Parse(mTotalGold.text);
			temp -= 15;
			mTotalGold.text = temp.ToString();
			NewValue(-1);
		}
	}

}
