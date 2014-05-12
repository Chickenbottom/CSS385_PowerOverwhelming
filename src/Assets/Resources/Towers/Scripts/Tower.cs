using UnityEngine;
using System.Collections;

public enum TowerType {
	kAbility,
	kUnitSpawner
}

public abstract class Tower : MonoBehaviour {
    public const int towerSpriteOffset = 4;

    protected int health;
    protected TowerType type;
    public int towerSprite;
	public GameObject TowerSelector = null;
	public UnitType UnitSpawnType;
		
    public abstract void Click();

    void OnMouseDown()
    {
		Debug.Log("CLICKED TOWER! " + this.type.ToString());
		GameObject.Find("GameManager").GetComponent<MouseManager>().Select(this);
    }
    
    /// <summary>
	/// Action performed when the user right-clicks on a location after
	/// selecting the tower
	/// </summary>
    /// <param name="location">Location. The game coodinate clicked on.</param>
    public abstract void SetTarget(Vector3 location);
    
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
