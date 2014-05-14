using UnityEngine;
using System.Collections;

public class UnitSpawnTower : TowerBehavior {

	// private SquadManager squads;
	public float mSpawnInterval = 30f;
	public UnitType mUnityType;
	float mPreviousSpawn = 0f;

	public float mSpawnBonus{ get; set; }
	public float mXP {get; set;}
	public float mXPMax {get; set;}
	public float mXPBonus {get; set;}

	float mXPperKill = 5f;

	SquadManager mSquadManager;


	void Start()
	{
		type = TOWERTYPE.Unit;
		loyalty = LOYALTY.Rodelle;
		health = 100;
		mMaxHealth = 100f;
		mXP = 0f;
		mXPBonus = 1f;
		mXPMax = 100f;
		SharedStart();
		mSquadManager = GameObject.Find("SquadManager").GetComponent<mSquadManager>();
	}
	void Update()
	{
		if ((Time.realtimeSinceStartup - previousSpawn) > mSpawnInterval * spawnBonus){
			mSquadManager.SpawnSquadFromUnitType(mUnityType); 
		}
		if(Input.GetMouseButtonUp(1)){
			rightClicked = this.gameObject;
		}
	}
	
	public override void Click(/*should pass mouse position*/)
	{
		Debug.Log("YAY IT WORKED" + health + "  " + type);
		mSquadManager.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

    public override bool ValidMousePos(Vector3 mousePos)
    {
        return GameObject.Find("ClickBox").GetComponent<ClickBox>().GetBounds().contains(mousePos);
    }

	public void IncraseXP(){
		mXP += mXPperKill * mXPBonus;
		if(mXP >= mXPMax)
			mXP = mXPMax;
	}
}
