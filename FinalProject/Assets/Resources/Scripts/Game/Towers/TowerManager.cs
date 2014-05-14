using UnityEngine;
using System.Collections;

	public enum TOWERTYPE{
		Ability,
		Unit,
	};

public abstract class TowerBehavior : MonoBehaviour {


	public enum SPAWNTYPE{
		Melee,
		Ranged,
		Magic,
	};


	public enum LOYALTY
	{
		Rodelle,
		Peasant,
	};

	public const int towerSpriteOffset = 4;

	protected TOWERTYPE type;
	public int towerSprite;
	protected LOYALTY loyalty;

	protected GameObject rightClicked{ get; set; }
	public float health{ get; set; }
	public float mMaxHealth{get; set;}

	abstract void Click();
    abstract bool ValidMousePos(Vector3 mousePos);
	//abstract void getMyTowerType();

	void OnMouseDown()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("CLICKED TOWER!");
			GameObject.Find("GameManager").GetComponent<MouseManager>().Select(this.gameObject);
		}
	}
	
	public TOWERTYPE GetTowerType()
	{
		return type;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "something(Clone)")
			health -= GameObject.Find("something(Clone)").GetComponent<EnemyAIManager>().getDamageRate();
	}

	public void Damage(int damage)
	{
		// change sprite int here
	}
	
	protected void SharedStart()
	{
		GameObject.Find("GameManager").GetComponent<EnemyAIManager>().AddTarget(this.gameObject);
	}

}
