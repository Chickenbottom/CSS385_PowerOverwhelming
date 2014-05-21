using UnityEngine;
using System.Collections;

public class StoreCloseButton : ButtonBehaviour {

	public GameObject TowerStoreFrame;
	void OnMouseDown(){
		ChangeScreen();
		TowerStoreFrame.GetComponent<TowerStoreBehavior>().Purchase();
		TowerStoreFrame.SetActive(false);
	}
}
