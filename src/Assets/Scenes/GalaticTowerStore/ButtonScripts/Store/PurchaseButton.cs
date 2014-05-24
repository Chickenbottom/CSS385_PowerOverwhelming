using UnityEngine;
using System.Collections;

public class PurchaseButton : ButtonBehaviour {
	public GameObject TowerStoreFrame;
	public GUIText mTotalGoal;	

	void Start(){
		mTotalGoal.text = GameState.Gold.ToString();
	}
	void OnMouseDown(){
		TowerStoreFrame.GetComponent<TowerStoreBehavior>().Purchase();
		mTotalGoal.text = "0";
	}
}
