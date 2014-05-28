using UnityEngine;
using System.Collections;

public class PurchaseButton : ButtonBehaviour {
	public GameObject TowerStoreFrame;
	public GUIText mTotalGold;	
	public GUIText mTotalCost;
	bool canAfford = true;
	
	void Start(){
		//remove for game
		//GameState.Gold = 100;
		mTotalCost.text = "0";
	}
	void Update(){
		mTotalGold.text = GameState.Gold.ToString();
		if(TowerStoreFrame.GetComponent<TowerStoreBehavior>().mCurCost > GameState.Gold){
			mTotalCost.color = Color.red;
			canAfford = false;
		}
		else{
			mTotalCost.color = Color.white;
			canAfford = true;
		}
	}
	void OnMouseDown(){
		if(canAfford){
			TowerStoreFrame.GetComponent<TowerStoreBehavior>().Purchase();
			mTotalGold.text = "0";
		}
	}
}
