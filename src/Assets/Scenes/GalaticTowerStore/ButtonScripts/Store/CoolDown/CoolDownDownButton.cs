using UnityEngine;
using System.Collections;

public class CoolDownDownButton : CoolDown {


	
	void OnMouseDown(){
		if( ( mBonusLevel - 1) > 0 || ( mBonusLevel - 1 ) > GetOriginal())  
			NewValue(-1);
	}
}
