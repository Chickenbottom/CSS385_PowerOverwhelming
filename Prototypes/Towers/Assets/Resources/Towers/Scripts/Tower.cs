using UnityEngine;
using System.Collections;

public abstract class Tower : MonoBehaviour {
    
    public enum TowerType {
        kAbility,
        kUnitSpawner
    }

    public const int towerSpriteOffset = 4;

    protected int health;
    protected TowerType type;
    public int towerSprite;
	public GameObject TowerSelector = null;
	
    public abstract void Click();

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLICKED TOWER! " + this.type.ToString());
            GameObject.Find("GameManager").GetComponent<MouseManager>().Select(this);
        }
    }
    
    public void ShowSelector(bool status)
    {
		if (TowerSelector == null)
			return;
			
		Renderer r = TowerSelector.gameObject.GetComponent<Renderer>();
		r.enabled = status;
	}
		
    public TowerType GetTowerType()
    {
        return type;
    }

    public void Damage(int damage)
    {
        // change sprite int here
    }
    
    void Awake()
    {
		this.ShowSelector(false);
    }
}
