using UnityEngine;
using System.Collections;

public class PurchaseButton : ButtonBehaviour {
	public GameObject TowerStoreFrame;
	public GUIText mTotalGoal;	
	bool canAfford = true;
	public Sprite mStoreClosed;
	
	void Start(){
		GameState.Gold = 100;

		mTotalGoal.text = GameState.Gold.ToString();
	}
	void Update(){
		if(TowerStoreFrame.GetComponent<TowerStoreBehavior>().mCurCost > GameState.Gold){
			mTotalGoal.color = Color.red;
			canAfford = false;
		}
		else{
			mTotalGoal.color = Color.white;
			canAfford = true;
		}
	}
	void OnMouseDown(){
		if(canAfford){
			TowerStoreFrame.GetComponent<TowerStoreBehavior>().Purchase();
			mTotalGoal.text = "0";
		}
	}
}
