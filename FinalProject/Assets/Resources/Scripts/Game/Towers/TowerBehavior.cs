using UnityEngine;
using System.Collections;

public abstract class TowerBehavior : MonoBehaviour {
    
    public enum TOWERTYPE {
        Ability,
        Unit
    }

    private int health;
    private TOWERTYPE type;

    public abstract void Click();
    public abstract TOWERTYPE GetTowerType();

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnTriggerEnter2D()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("GameManager").GetComponent<MouseManager>().Select(this.gameObject);
        }
    }

}
