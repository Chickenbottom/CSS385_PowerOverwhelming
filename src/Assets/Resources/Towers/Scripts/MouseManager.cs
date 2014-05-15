using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseManager : MonoBehaviour
{

    #region variables
    private Tower mTowerSelected;
    private bool mSelected;
    #endregion

	public List<Tower> Towers;
	
    void Start()
    {
        mSelected = false;
        mTowerSelected = null;
    }

    void Update()
    {        
        if (Input.GetMouseButtonDown(1) && mSelected) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
			if (ValidMousePos(mousePos))
				mTowerSelected.SetTarget(mousePos);
        }
        
		if (Input.GetButtonDown("Fire2") && mTowerSelected is UnitSpawningTower) {
			((UnitSpawningTower)mTowerSelected).SpawnUnit();
		}
	}
	
	private bool ValidMousePos(Vector3 mousePos)
    {
        // Add a dead spot around the towers to prevent sending units when you meant to reselect a tower
        return mousePos.x < 240f && mousePos.x > -250f && mousePos.y < 102f && mousePos.y > -169f;
    }

    public void Select(Tower tower)
    {
        mTowerSelected = tower;
        tower.ShowSelector(true);
        
        foreach (Tower t in Towers) {
			if (t != mTowerSelected)
				t.ShowSelector(false);
        }
        mSelected = true;
    }

}
