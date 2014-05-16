using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseManager : MonoBehaviour
{
	///////////////////////////////////////////////////////////////////////////////////
	// Inspector Presets
	///////////////////////////////////////////////////////////////////////////////////
	public List<Tower> Towers;
	
	///////////////////////////////////////////////////////////////////////////////////
	// Public
	///////////////////////////////////////////////////////////////////////////////////
	
	public void SelectTower(Tower selectedTower)
	{
		foreach (Tower t in Towers) {
			if (t != selectedTower)
				t.ShowSelector(false);
		}
			
		mSelectedTower = selectedTower;
		mSelectedTower.ShowSelector(true);
		mWasJustSelected = true;
	}
		
	public void SetAbilityTarget(Target target)
	{
		mSelectedTower.UseTargetedAbility(target);
		mWasJustSelected = true;
	}
	
	///////////////////////////////////////////////////////////////////////////////////
	// Private
	///////////////////////////////////////////////////////////////////////////////////
	
	private Tower mSelectedTower;
	private Tower towerClicked;
	private bool mWasJustSelected;
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	///////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
		if (Towers == null || Towers.Count == 0)
			Debug.LogError("The towers need to be added to the Mouse Manager in the Unity Inspector.");
			
        SelectTower(Towers[0]);
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && ! mWasJustSelected) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            mSelectedTower.SetTarget(mousePos);
        }
        
		if (Input.GetMouseButtonDown(1) && ! mWasJustSelected)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0f;
			mSelectedTower.UsePositionalAbility(mousePos);
		}
		
		if (Input.GetButtonDown("Fire2") && mSelectedTower is UnitSpawningTower) {
			((UnitSpawningTower)mSelectedTower).SpawnUnit();
		}
		
		mWasJustSelected = false;
	}
}
