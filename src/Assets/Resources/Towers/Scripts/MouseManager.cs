using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseManager : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Public Methods and Variables
    ///////////////////////////////////////////////////////////////////////////////////
    
    public bool RodelleClicked { get; set; }
        
    public void TowerClicked (Tower tower)
    {
        mClickedTower = tower;
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private Methods and Variables
    ///////////////////////////////////////////////////////////////////////////////////
    
    private Tower mSelectedTower = null;
    private Tower mClickedTower = null;
    
    private void SelectTower (Tower t)
    {
        mSelectedTower = t;
        mSelectedTower.ShowSelector (true);
    }

    private void DeSelectTower ()
    {
        mSelectedTower.ShowSelector (false);
        mSelectedTower = null;
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    /////////////////////////////////////////////////////////////////////////////////// 
    
    void Update ()
    {
        // Must be called last in the script order to work 100% of the time
        if (Input.GetMouseButtonDown (0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            mousePos.z = 0f;
            if (mSelectedTower != null) {
                if (mSelectedTower.canTargetTowers) {
                    if (mClickedTower != null) {
                        if (mClickedTower != mSelectedTower) {
                            mSelectedTower.SetTarget (mousePos);
                        }
                        DeSelectTower ();
                    } else { // towerClicked == null
                        if (mSelectedTower.ValidMousePos (mousePos)) {
                            mSelectedTower.SetTarget (mousePos);
                            DeSelectTower ();
                        }
                        // else do nothing
                    }
                } else { // can NOT target towers
                    if (mClickedTower != null) {
                        DeSelectTower ();
                        SelectTower (mClickedTower);
                    } else { // did NOT click on a tower
                        //Debug.Log("HERE");
                        if (mSelectedTower.ValidMousePos (mousePos)) {
                            mSelectedTower.SetTarget (mousePos);
                            DeSelectTower ();
                        }
                    }
                }
            } else { // towerSelected == null
                if (mClickedTower != null) {
                    SelectTower (mClickedTower);
                }
                // else do nothing
            }
        }
        
        if (Input.GetButtonDown ("Fire2") && mSelectedTower is UnitSpawningTower) {
            ((UnitSpawningTower)mSelectedTower).SpawnUnit ();
        }
        
        if (Input.GetButtonDown ("SelectRanged"))
            SelectTower (GameObject.Find ("ArcherTower").GetComponent<Tower> ());
        
        if (Input.GetButtonDown ("SelectMelee"))
            SelectTower (GameObject.Find ("SwordsmanTower").GetComponent<Tower> ());
        
        if (Input.GetButtonDown ("SelectSpecial"))
            SelectTower (GameObject.Find ("MageTower").GetComponent<Tower> ());
        
        if (Input.GetButtonDown ("SelectAbility1"))
            SelectTower (GameObject.Find ("AbilityTower").GetComponent<Tower> ());
        
        mClickedTower = null;
    }

}
