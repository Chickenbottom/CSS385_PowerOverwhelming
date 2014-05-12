using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadManager : MonoBehaviour 
{
	///////////////////////////////////////////////////////////////////////////////////
	// Public Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
	public UnitType SquadType = UnitType.kNone;
	
	public void SetDestination(Vector3 location)
	{
		location.z = 0;
		
		List<Vector3> randomPositions = this.RandomSectionLocations(mSquads.Count, kSquadWidth);
		
		this.transform.position = location;
		
		for (int i = mSquads.Count - 1; i >= 0; --i) {
			if (mSquads[i] == null) {
				mSquads.RemoveAt(i);
				continue;
			}
			Vector3 moveTo = location;
			moveTo += randomPositions[i];
			mSquads[i].UpdateSquadDestination(moveTo);
		}
	}
	
	public void AddSquad(Squad squad)
	{
		mSquads.Add (squad);
		this.SetDestination(mRallyPoint);
	}
	
	public void AddSquad(UnitType unitType = UnitType.kNone)
	{
		// nothing to instantiate
		if (unitType == UnitType.kNone && this.SquadType == UnitType.kNone)
			Debug.LogError("SquadManager - spawn type not specified.");
			
		Squad squad;
		if (unitType == UnitType.kNone)
			squad = SpawnSquadFromUnitType(this.SquadType);
		else
			squad = SpawnSquadFromUnitType(unitType);
			
		mSquads.Add (squad);
		this.SetDestination(mRallyPoint);
	}
	
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods and Variables
	///////////////////////////////////////////////////////////////////////////////////
	
	private List<Squad> mSquads;
	private float kSquadWidth = 8f; // TODO replace with function calculating width of squad
	private Vector3 mRallyPoint;
	private static GameObject mSquadPrefab = null;
	
	private Squad SpawnSquadFromUnitType(UnitType unitType)
	{
		GameObject o = (GameObject) Instantiate(mSquadPrefab);
		Squad squad = (Squad) o.GetComponent(typeof(Squad));
		squad.UnitType = unitType;
		squad.Spawn();
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
		if (mSquadPrefab == null)
			mSquadPrefab = Resources.Load ("Squads/SquadPrefab") as GameObject;
		
		mSquads = new List<Squad>();
		mRallyPoint = this.transform.position;
	}
	
	void Update () 
	{
		// only capture input in squad testing scene 
		if (!Application.loadedLevelName.Equals("SquadTest"))
			return;
			
		if (Input.GetButtonDown("Fire1")) {
			this.AddSquad();
		}
		
		// Move the squads to the location clicked on the screen
		if (Input.GetMouseButtonDown(0)) {
			mRallyPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mRallyPoint.z = 0;
			SetDestination(mRallyPoint);
		}
	}
	
}