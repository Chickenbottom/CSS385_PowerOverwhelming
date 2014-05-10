using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squad : MonoBehaviour 
{
	GameObject mUnitPrefab;
	GameObject mEnemyPrefab;
	
	Squad mTargetSquad;
	
	private List<Unit> mSquadMembers;
	
	public int NumSquadMembers;
	public string UnitPrefab;
	
	// Squad members form concentric circles around the squad center
	// the member width is used to determine the width of each band of the circles
	// ie. inner circle radius = kSquadMemberWidth * 1.5
	//     2nd circle width = kSquadMemberWidth * 1.5
	private const float kSquadMemberWidth = 3.0f;
	
	public Vector3 RallyPoint;
	
	public bool IsEngaged;
	
	// Use this for initialization
	void Awake () {
		if (null == mUnitPrefab) 
			mUnitPrefab = Resources.Load(UnitPrefab) as GameObject;
		
		if (mSquadMembers != null) {
			foreach (Unit u in mSquadMembers)
				Destroy(u.gameObject);
		}
			
		mSquadMembers = new List<Unit>();
		
		List<Vector3> randomPositions = this.RandomSectionLocations(NumSquadMembers, kSquadMemberWidth * 1.5f);
		
		// Instantiates and initializes the position of each member in the squad
		// TODO fix the placement for large numbers of squad members
		for (int i = 0; i < NumSquadMembers; ++i) {
			// instantiate the unit from the prefab
			GameObject o = (GameObject) Instantiate(mUnitPrefab);
			Unit u = (Unit) o.GetComponent(typeof(Unit));
			u.Squad = this;
			mSquadMembers.Add (u);
			
			// offset from squad center
			Vector3 memberPosition = this.transform.position;
			memberPosition += randomPositions[i];
			u.transform.position = memberPosition;
		}
	}
	
	public void NotifyUnitDied(Unit who)
	{
		mSquadMembers.Remove(who);
		NumSquadMembers --;
	}
	
	// TODO replace GameObject with EnemyUnit base type
	public void NotifyEnemySighted(Unit who, GameObject enemyUnit)
	{
		IsEngaged = true;
		Unit u = (Unit) enemyUnit.GetComponent(typeof(Unit));
		mTargetSquad = u.Squad;
		
		List<Unit> mEnemies = mTargetSquad.mSquadMembers;
		int numEnemies = mEnemies.Count;
		
		// Surround the enemy!
		List<Vector3> positions = SurroundingPositions(
			enemyUnit.transform.position, 
			who.transform.position, 
			NumSquadMembers, 
			kSquadMemberWidth * 2.0f, 
			who.Range);
		
		for(int i = 0; i < mSquadMembers.Count; ++i) {
			mSquadMembers[i].collider2D.enabled = false;
			mSquadMembers[i].Engage(mEnemies[i % numEnemies], positions[i]);
		}
	}
	
	public void NotifyEnemyKilled(Unit who)
	{
		List<Unit> mEnemies = mTargetSquad.mSquadMembers;
		int numEnemies = mEnemies.Count;
		
		if (numEnemies == 0)
			DisengageSquad();
		else 
			who.Engage(mEnemies[Random.Range (0, numEnemies)]); // engage random enemy
	}
	
	// re-enable the search for new enemies
	private void DisengageSquad()
	{
		IsEngaged = false;
		for(int i = 0; i < mSquadMembers.Count; ++i) {
			mSquadMembers[i].collider2D.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// only capture input in squad testing scene 
		if (!Application.loadedLevelName.Equals("SquadTest"))
			return;
		
		if (this.UnitPrefab == "Squads/Prefabs/PeasantPrefab" && Input.GetButtonDown("Fire2")) {
			this.NumSquadMembers = 10;
			this.Awake();
		}
		
		/*if (Input.GetMouseButtonDown(0))
			UpdateSquadDestination (Camera.main.ScreenToWorldPoint(Input.mousePosition));
			
		if (Input.GetButtonDown("Fire2"))
			this.Awake();
			*/
	}
	
	public void UpdateSquadDestination(Vector3 location) 
	{
		RallyPoint = location;
		location.z = 0;
		
		this.transform.position = location;
		
		// Randomize the squad's new position around the central location
		List<Vector3> randomPositions = 
			RandomSectionLocations(NumSquadMembers, kSquadMemberWidth * 1.5f);
		
		// Move each squad member to their new location
		for (int i = 0; i < NumSquadMembers; ++i) {
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
		
		randomLocations.Add(Vector3.zero);
		// assigns a random position in the section
		for (int i = 1; i < numSections; ++i) {
			float angle = randomAngles[i];
			Vector3 randomDir = new Vector3(Mathf.Cos (angle), Mathf.Sin (angle), 0f);
			randomDir.Normalize();
			randomDir *= Random.Range (unitWidth, unitWidth * 3f);
			randomLocations.Add(randomDir);
		}
		
		return randomLocations;
	}
	
	/**
	Creates vectors to points on a circle around the target
	  
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
			newPosition.x = r * Mathf.Cos(angles[i]);
			newPosition.y = r * Mathf.Sin(angles[i]);
						
			surroundingPositions.Add (newPosition);
		}
		
		return surroundingPositions;
	}
}
