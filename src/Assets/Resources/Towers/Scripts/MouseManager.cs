using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseManager : MonoBehaviour
{
    #region variables
    private Tower towerSelected;
    private Tower towerClicked;
    private bool rodelleClicked;
    #endregion
    
    void Start ()
    {
        towerSelected = null;
        towerClicked = null;
    }

    void Update ()
    {
        // Must be called last in the script order to work 100% of the time
        if (Input.GetMouseButtonDown (0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            mousePos.z = 0f;
            if (towerSelected != null) {
                if (towerSelected.canTargetTowers) {
                    if (towerClicked != null) {
                        if (towerClicked != towerSelected) {
                            towerSelected.SetTarget (mousePos);
                        }
                        DeSelectTower ();
                    } else { // towerClicked == null
                        if (towerSelected.ValidMousePos (mousePos)) {
                            towerSelected.SetTarget (mousePos);
                            DeSelectTower ();
                        }
                        // else do nothing
                    }
                } else { // can NOT target towers
                    if (towerClicked != null) {
                        DeSelectTower ();
                        SelectTower (towerClicked);
                    } else { // did NOT click on a tower
                        //Debug.Log("HERE");
                        if (towerSelected.ValidMousePos (mousePos)) {
                            towerSelected.SetTarget (mousePos);
                            DeSelectTower ();
                        }
                    }
                }
            } else { // towerSelected == null
                if (towerClicked != null) {
                    SelectTower (towerClicked);
                }
                // else do nothing
            }

        }
        
        if (Input.GetButtonDown ("Fire2") && towerSelected is UnitSpawningTower) {
            ((UnitSpawningTower)towerSelected).SpawnUnit ();
        }
        
        if (Input.GetButtonDown("SelectRanged"))
            SelectTower(GameObject.Find("ArcherTower").GetComponent<Tower>());
            
        if (Input.GetButtonDown("SelectMelee"))
            SelectTower(GameObject.Find("SwordsmanTower").GetComponent<Tower>());
            
        if (Input.GetButtonDown("SelectSpecial"))
            SelectTower(GameObject.Find("MageTower").GetComponent<Tower>());
            
        if (Input.GetButtonDown("SelectAbility1"))
            SelectTower(GameObject.Find("AbilityTower").GetComponent<Tower>());
        
        towerClicked = null;
        rodelleClicked = false;
    }

    public void TowerClicked (Tower tower)
    {
        towerClicked = tower;
    }

    private void SelectTower (Tower t)
    {

        towerSelected = t;
        towerSelected.ShowSelector (true);
    }

    private void DeSelectTower ()
    {
        towerSelected.ShowSelector (false);
        towerSelected = null;
    }

    public bool RodelleClicked { get; set; }

}
