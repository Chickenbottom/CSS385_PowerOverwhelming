using UnityEngine;
using System.Collections;

public class TowerHealDownButton : TowerHeal {

	void OnMouseDown(){
		if( ( BonusLevel - 1) > 0 || ( BonusLevel - 1 ) > GetOriginal()) {
			int temp = int.Parse(mTotalGold.text);
			temp -= 10;
			mTotalGold.text = temp.ToString();
			NewValue(-1);
		}
	}
}
