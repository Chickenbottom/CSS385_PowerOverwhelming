using UnityEngine;
using System.Collections;

public abstract class TowerBehavior : MonoBehaviour {
    
    public enum TOWERTYPE {
        Ability,
        Unit
    }

    public const int towerSpriteOffset = 4;

    protected int health;
    protected TOWERTYPE type;
    public int towerSprite;

    public abstract void Click();

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

    public void Damage(int damage)
    {
        // change sprite int here
    }

}
