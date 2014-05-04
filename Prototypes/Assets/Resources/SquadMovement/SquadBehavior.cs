using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadBehavior : MonoBehaviour {

	GameObject mUnitPrefab;

	private List<SquadUnit> mSquadMembers;
	private const int kNumMembersInSquad = 4;

	// Squad members form concentric circles around the squad center
	// the member width is used to determine the width of each band of the circles
	// ie. inner circle radius = kSquadMemberWidth * 1.5
	//     2nd circle width = kSquadMemberWidth * 1.5
	private const float kSquadMemberWidth = 2.0f;

	// Use this for initialization
	void Awake () {
		if (null == mUnitPrefab) 
			mUnitPrefab = Resources.Load("SquadMovement/SquadUnit") as GameObject;
			
		mSquadMembers = new List<SquadUnit>();
		
		List<Vector3> randomPositions = this.RandomSectionLocations(kNumMembersInSquad, kSquadMemberWidth * 1.5f);
		
		// Instantiates and initializes the position of each member in the squad
		// TODO fix the placement for large numbers of squad members
		for (int i = 0; i < kNumMembersInSquad; ++i) {
			// instantiate the unit from the prefab
			GameObject o = (GameObject) Instantiate(mUnitPrefab);
			SquadUnit u = (SquadUnit) o.GetComponent(typeof(SquadUnit));
			mSquadMembers.Add (u);
			
			// offset from squad center
			Vector3 memberPosition = this.transform.position;
			memberPosition += randomPositions[i];
			u.transform.position = memberPosition;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// only capture input in single squad movement prototype
		if (!Application.loadedLevelName.Equals("SquadMovement"))
			return;
			
		if (Input.GetMouseButtonDown(0))
			MoveSquad (Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}
	
	public void MoveSquad(Vector3 location) 
	{
		location.z = 0;
		
		this.transform.position = location;
		
		// Randomize the squad's new position around the central location
		List<Vector3> randomPositions = 
			RandomSectionLocations(kNumMembersInSquad, kSquadMemberWidth * 1.5f);
			
		// Move each squad member to their new location
		for (int i = 0; i < kNumMembersInSquad; ++i) {
			Vector3 memberPosition = this.transform.position;
			memberPosition += randomPositions[i];
			mSquadMembers[i].MoveTo(memberPosition);
		}
	}
	
	// Creates random directions for squad members to form a concentric circle around the target location
	// TODO fix the placement for large numbers of squad members
	// TODO create location in center for odd number of members?
	private List<Vector3> RandomSectionLocations(int numSections, float circleWidth)
	{
		List<Vector3> randomLocations = new List<Vector3>();
		
		List<float> randomAngles = new List<float>();
		float anglesPerSection = 360f / numSections;
		for (int i = 0; i < numSections; ++i) {
			float randomAngle = Random.Range(anglesPerSection * .2f, anglesPerSection * 0.8f);
			randomAngle += i * anglesPerSection;
			randomAngles.Add(randomAngle * Mathf.Deg2Rad);
		}
		
		// assigns a random position in the section
		for (int i = 0; i < numSections; ++i) {
			float angle = randomAngles[i];
			Vector3 randomDir = new Vector3(Mathf.Cos (angle), Mathf.Sin (angle), 0f);
			randomDir.Normalize();
			randomDir *= Random.Range (circleWidth, circleWidth * 3f);
			randomLocations.Add(randomDir);
		}
		
		return randomLocations;
	}
}
