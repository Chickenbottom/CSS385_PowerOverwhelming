using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseManager : MonoBehaviour
{

    #region variables
    private Tower towerSelected;
    private bool selected;
    private ClickBox clickBox;
    private bool towerSelectionChanged;
    #endregion

	public List<Tower> Towers;
	
    void Start()
    {
        selected = false;
        towerSelected = null;
        clickBox = GameObject.Find("ClickBox").GetComponent<ClickBox>();
        towerSelectionChanged = false;
    }

    void Update()
    {        
        // Only sends click data if a tower is selected + one wasn't selected this turn
        // MouseManager must execute after all the tower colliders
        if (!towerSelectionChanged && Input.GetMouseButtonDown(0) && selected) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
			if (ValidMousePos(mousePos))
				towerSelected.SetTarget(mousePos);
        }
        
		if (Input.GetButtonDown("Fire2") && towerSelected is UnitSpawningTower) {
			((UnitSpawningTower)towerSelected).SpawnUnit();
		}
        towerSelectionChanged = false;
	}
	
	private bool ValidMousePos(Vector3 mousePos)
    {
        // Will be put into Tower subclasses
        return clickBox.GetClickBoxBounds().Contains(mousePos);
    }

    public void Select(Tower tower)
    {
        towerSelected = tower;
        tower.ShowSelector(true);
        
        foreach (Tower t in Towers) {
			if (t != towerSelected)
				t.ShowSelector(false);
        }
        selected = true;
        towerSelectionChanged = true;
    }

}
