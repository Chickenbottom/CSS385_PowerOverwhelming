using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseManager : MonoBehaviour
{

    #region variables
    private Tower towerSelected;
    private bool selected;
    private bool towerSelectionChanged;
    #endregion

	public List<Tower> Towers;
	
    void Start()
    {
        selected = false;
        towerSelected = null;
        towerSelectionChanged = false;
    }

    void Update()
    {        
        // Only sends click data if a tower is selected + one wasn't selected this turn
        // MouseManager must execute after all the tower colliders
        if (!towerSelectionChanged && Input.GetMouseButtonDown(0) && selected) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            if (towerSelected.ValidMousePos(mousePos))
            {
                towerSelected.SetTarget(mousePos);
                towerSelected.ShowSelector(false);
                towerSelected = null;
                selected = false;
            }
        }
        
		if (Input.GetButtonDown("Fire2") && towerSelected is UnitSpawningTower) {
			((UnitSpawningTower)towerSelected).SpawnUnit();
		}
        towerSelectionChanged = false;
	}

    public void Select(Tower tower)
    {
        if (towerSelected != null)
        {
            towerSelected.ShowSelector(false);
        }
        towerSelected = tower;
        tower.ShowSelector(true);
        selected = true;
        towerSelectionChanged = true;
    }

}
