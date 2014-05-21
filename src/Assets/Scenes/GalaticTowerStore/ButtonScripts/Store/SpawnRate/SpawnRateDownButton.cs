using UnityEngine;
using System.Collections;

public class SpawnRateDownButton : SpawnRate {


	void OnMouseDown(){
		if( ( BonusLevel - 1) > 0 || ( BonusLevel - 1 ) > GetOriginal())  
		NewValue(-1);
	}
}
