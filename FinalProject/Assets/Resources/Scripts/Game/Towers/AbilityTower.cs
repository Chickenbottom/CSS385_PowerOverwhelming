using UnityEngine;
using System.Collections;

public class AbilityTowerBehavior: TowerBehavior {

    // TODO: Abstract the abilities out of the AbilityTower class 
    // into an Ability class with subclasses for each ability

	public float mAbilityCooldown = 30f;
	float mHealingAbility = 25f;
	float mSpawnBonus = 1f;

    private Bounds[] mTargetBounds;
    private GameObject[] mTargets;

	public float mHealthBonus{get; set;}


	public override void Click()
	{
        // This function for the heal tower will no longer do anything
		Debug.Log("CLICKED" + type + " tower " + health );
		try{

			//rightClicked.GetComponent<TowerBehavior>().health += mHealingAbility * mHealthBonus;
		}
  	   	catch(UnityException e)
		{}
	   
	}
	
	void Start()
	{
		type = TOWERTYPE.Ability;
		health = 100;
		SharedStart();
		InvokeRepeating("AbilityCoolDown", 1f,1f);
	
        mTargets = new GameObject[4] {this.gameObject, GameObject.Find("Tower1"), GameObject.Find("Tower2"), GameObject.Find("Tower3")};
        mTargetBounds = new Bounds[4] { getBounds(mTargets[0]), getBounds(mTargets[1]), getBounds(mTargets[2]), getBounds(mTargets[3])};

    }

    private Bounds getBounds(GameObject g)
    {
        BoxCollider2D temp = g.GetComponent<BoxCollider2D>();
        return new Bounds(new Vector3(temp.center.x, temp.center.y, 0f), new Vector3(temp.size.x, temp.size.y, 0f));
    }

	void Update()
	{
		if(Input.GetMouseButtonUp(1)){
			rightClicked = this.gameObject;
		}
	}
	void AbilityCoolDown(){
		mAbilityCooldown -= 1;
		if(mAbilityCooldown == 0)
			mAbilityCooldown = 0;
	}

    public override bool ValidMousePos(Vector3 mousePos)
    {
        for (int i = 0; i < mTargetBounds.Length; i++)
        {
            if (mTargetBounds[i].Contains(mousePos))
            {
                // Heal here and just use Click() as a way to reset the MouseManagers flags
                // use mTarget[i]
                return true;
            }
        }
        return false;
    }

}
