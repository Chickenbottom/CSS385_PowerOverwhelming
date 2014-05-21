using UnityEngine;
using System.Collections;

public class SpawnSizeUpButton : SpawnSize {

	const int kBonusMax = 5;
	void OnMouseDown(){
		if( (BonusLevel+1) <= kBonusMax )
		NewValue(1);
	}
}
