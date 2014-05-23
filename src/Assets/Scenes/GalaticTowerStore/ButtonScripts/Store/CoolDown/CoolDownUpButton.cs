using UnityEngine;
using System.Collections;

public class CoolDownUpButton : CoolDown {

	const int kBonusMax = 5;
	void OnMouseDown(){
		if( (mBonusLevel+1) <= kBonusMax )
			NewValue(1);
	}
}
