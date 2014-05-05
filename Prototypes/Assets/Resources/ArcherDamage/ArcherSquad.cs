using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherSquad : MonoBehaviour 
{
	GameObject mUnitPrefab;
	GameObject mEnemyPrefab;
	
	private List<ArcherUnit> mSquadMembers;
	private const int kNumMembersInSquad = 4;
	
	// Squad members form concentric circles around the squad center
	// the member width is used to determine the width of each band of the circles
	// ie. inner circle radius = kSquadMemberWidth * 1.5
	//     2nd circle width = kSquadMemberWidth * 1.5
	private const float kSquadMemberWidth = 3.0f;
	
	// Use this for initialization
	void Awake () {
		if (null == mUnitPrefab) 
			mUnitPrefab = Resources.Load("ArcherDamage/ArcherUnit") as GameObject;
			
		
		if (null == mEnemyPrefab) 
			mEnemyPrefab = Resources.Load("ArcherDamage/ArcherEnemy") as GameObject;
		
		mSquadMembers = new List<ArcherUnit>();
		
		List<Vector3> randomPositions = this.RandomSectionLocations(kNumMembersInSquad, kSquadMemberWidth * 1.5f);
		
		// Instantiates and initializes the position of each member in the squad
		// TODO fix the placement for large numbers of squad members
		for (int i = 0; i < kNumMembersInSquad; ++i) {
			// instantiate the unit from the prefab
			GameObject o = (GameObject) Instantiate(mUnitPrefab);
			ArcherUnit u = (ArcherUnit) o.GetComponent(typeof(ArcherUnit));
			u.Squad = this;
			mSquadMembers.Add (u);
			
			// offset from squad center
			Vector3 memberPosition = this.transform.position;
			memberPosition += randomPositions[i];
			u.transform.position = memberPosition;
		}
	}
	
	// TODO replace GameObject with EnemyUnit base type
	public void NotifyEnemySighted(ArcherUnit who, GameObject enemyUnit)
	{
		// Surround the enemy!
		List<Vector3> positions = SurroundingPositions(
			enemyUnit.transform.position, 
			who.transform.position, 
			kNumMembersInSquad, 
			kSquadMemberWidth * 2.0f, 
			who.Range);
			
		for(int i = 0; i < mSquadMembers.Count; ++i) {
			mSquadMembers[i].collider2D.enabled = false;
			mSquadMembers[i].ShootAt(enemyUnit, positions[i]);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// only capture input in single squad movement prototype
		if (!Application.loadedLevelName.Equals("ArcherDamage"))
			return;
		
		if (Input.GetMouseButtonDown(0))
			UpdateSquadDestination (Camera.main.ScreenToWorldPoint(Input.mousePosition));
			
		if (Input.GetButtonDown("Fire1")) {
			GameObject o = (GameObject) Instantiate(mEnemyPrefab);
			
			Vector3 randomPosition = new Vector3(Random.Range(-80f, 80f), Random.Range(-100f, 100f), 0);
			o.transform.position = randomPosition;
		}
	}
	
	public void UpdateSquadDestination(Vector3 location) 
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
	
	/**
	Creates random directions for squad members to form a concentric circle around the target location
	
	@param numSections: how many sections to divide the circle into
	@param unitWidth: the width of each unit
	
	TODO fix the placement for large numbers of squad members
	TODO create location in center for odd number of members?
	*/
	
	private List<Vector3> RandomSectionLocations(int numSections, float unitWidth)
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
			randomDir *= Random.Range (unitWidth, unitWidth * 3f);
			randomLocations.Add(randomDir);
		}
		
		return randomLocations;
	}
	
	/**
	Creates positions in a circle surrounding the target
	
	The positions are slightly randomized relative to the unit's width
	  
	@param x: target to surround
	@param y: the direction to surround from
	@param n: number of units surrounding target
	@param w: width of each unit
	@param r: radius of the circle
	*/
	private List<Vector3> SurroundingPositions(Vector3 x, Vector3 y, int n, float w, float r)
	{
		List<Vector3> surroundingPositions = new List<Vector3>();
	
		Vector3 toTarget = y - x;
		toTarget.Normalize();
		float sign = Vector3.Cross(Vector3.right, toTarget).normalized.z;
		float theta = Mathf.Acos(Vector3.Dot(Vector3.right, toTarget));
		theta *= Mathf.Rad2Deg * sign;
		
		float angleBetweenUnits = 2f * Mathf.Atan(0.5f * w / r) * Mathf.Rad2Deg;

		List<float> angles = new List<float>();
		
		float toggle = 1;
		for (int i = 0; i < n; ++i) {
			toggle *= -1; // flip back and forth
			theta += i * toggle * angleBetweenUnits;
			//Debug.Log ("theta " + theta);
			
			angles.Add (theta * Mathf.Deg2Rad);
		}
		
		for (int i = 0; i < n; ++i) {
			Vector3 newPosition = x;
			newPosition.x += r * Mathf.Cos(angles[i]);
			newPosition.y += r * Mathf.Sin(angles[i]);
			
			// slightly randomize the position
			newPosition += Random.insideUnitSphere * w;
			newPosition.z = 0;
			
			surroundingPositions.Add (newPosition);
		}
		
		return surroundingPositions;
	}
}
