using UnityEngine;
using System.Collections;

public class CoolDownDownButton : CoolDown {


	
	void OnMouseDown(){
		if( ( BonusLevel - 1) > 0 || ( BonusLevel - 1 ) > GetOriginal())  
			NewValue(-1);
	}
}
