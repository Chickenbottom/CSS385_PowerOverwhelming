﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadManager : MonoBehaviour 
{
	///////////////////////////////////////////////////////////////////////////////////
	// Public Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
	public UnitType squadType = UnitType.None;
	
	public void SetDestination(Vector3 location)
	{
		location.z = 0;
		
		List<Vector3> randomPositions = this.RandomSectionLocations(squads.Count, squadWidth);
		
		this.transform.position = location;
		rallyPoint = location;
		
		for (int i = squads.Count - 1; i >= 0; --i) {
			if (squads[i] == null) {
				squads.RemoveAt(i);
				continue;
			}
			Vector3 moveTo = location;
			moveTo += randomPositions[i];
			squads[i].UpdateSquadDestination(moveTo);
		}
	}
	
	public void AddSquad(Squad squad)
	{
		squads.Add (squad);
		this.SetDestination(rallyPoint);
	}
	
	public void AddSquad(Vector3 spawnLocation, UnitType unitType = UnitType.None)
	{
		// nothing to instantiate
		if (unitType == UnitType.None && this.squadType == UnitType.None)
			Debug.LogError("SquadManager - spawn type not specified.");
			
		Squad squad;
		if (unitType == UnitType.None)
			squad = SpawnSquadFromUnitType(spawnLocation, this.squadType);
		else
			squad = SpawnSquadFromUnitType(spawnLocation, unitType);
			
		squads.Add (squad);
		this.SetDestination(rallyPoint);
	}
	
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
	
	private List<Squad> squads;
	private float squadWidth = 8f; // TODO replace with function calculating width of squad
	private Vector3 rallyPoint;
	private static GameObject squadPrefab = null;
	
	private Squad SpawnSquadFromUnitType(Vector3 location, UnitType unitType)
	{
		GameObject o = (GameObject) Instantiate(squadPrefab);
		Squad squad = (Squad) o.GetComponent(typeof(Squad));
		squad.unitType = unitType;
		squad.Spawn(location, unitType, Allegiance.Rodelle);
		return squad;
	}
	
	// Creates random directions for squad members to form a concentric circle around the target location
	// TODO fix the placement for large numbers of squad members
	// TODO move this to a general utility class
	private List<Vector3> RandomSectionLocations(int numSections, float circleWidth)
	{
		List<Vector3> randomLocations = new List<Vector3>();
		
		List<float> randomAngles = new List<float>();
		float anglesPerSection = 360f / (numSections - 1);
		for (int i = 0; i < numSections; ++i) {
			float randomAngle = Random.Range(anglesPerSection * .3f, anglesPerSection * 0.7f);
			randomAngle += i * anglesPerSection;
			randomAngles.Add(randomAngle * Mathf.Deg2Rad);
		}
		
		randomLocations.Add(Vector3.zero); // place first squad in center
		// assigns a random position in the section
		for (int i = 1; i < numSections; ++i) {
			float angle = randomAngles[i];
			Vector3 randomDir = new Vector3(Mathf.Cos (angle), Mathf.Sin (angle), 0f);
			randomDir.Normalize();
			//randomDir *= circleWidth;
			randomDir *= Random.Range (circleWidth, circleWidth * 2f);
			randomLocations.Add(randomDir);
		}
		
		return randomLocations;
	}
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	///////////////////////////////////////////////////////////////////////////////////
	
	void Awake () 
	{
		if (squadPrefab == null)
			squadPrefab = Resources.Load ("Squads/SquadPrefab") as GameObject;
		
		squads = new List<Squad>();
		rallyPoint = this.transform.position;
	}
	
	void Update () 
	{
		//if (Input.GetButtonDown("Fire2")) {
	//		this.AddSquad();
	//	}
		
		// Move the squads to the location clicked on the screen
		/*
		if (Input.GetMouseButtonDown(0)) {
			mRallyPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mRallyPoint.z = 0;
			SetDestination(mRallyPoint);
		}
		*/
	}
	
}