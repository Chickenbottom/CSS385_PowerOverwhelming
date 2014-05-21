using UnityEngine;
using System.Collections;

public class SpawnRateUpButton : SpawnRate {
	const int kBonusMax = 5;
	void OnMouseDown(){
		if( (BonusLevel+1) <= kBonusMax )
		NewValue(1);
	}
}
