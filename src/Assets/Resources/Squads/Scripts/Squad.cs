using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SquadAction {
	kEnemySighted, 
	kWeaponChanged, 
	kTargetDestroyed, 
	kDestinationReached,
	kUnitDied
}

public enum UnitType {
	kNone,
	kSwordsman,
	kArcher,
	kMage,
	kPeasant,
}

public class Squad : MonoBehaviour 
{
	///////////////////////////////////////////////////////////////////////////////////
	// Public Methods
	///////////////////////////////////////////////////////////////////////////////////
	public Vector3 RallyPoint;
	public bool IsEngaged;
	public int NumSquadMembers = 0;
	public List<Unit> SquadMembers { get { return mSquadMembers; } }
	
	public UnitType UnitType;
	
	public void Notify(SquadAction action, params object[] args)
	{
		switch (action) {
		case(SquadAction.kEnemySighted):
			AttackTarget((Unit)args[0], (Target)args[1]);
			break;
			
		case(SquadAction.kDestinationReached):
			break;
			
		case(SquadAction.kTargetDestroyed):
			UpdateTarget((Unit)args[0]);
			break;
			
		case(SquadAction.kWeaponChanged):
			ChangeSquadWeapons((Unit)args[0], (int)args[1]);
			break;
			
		case (SquadAction.kUnitDied):
			UpdateSquadMembers((Unit)args[0]);
			break;
		}
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
	
	public void Spawn(Vector3 location, UnitType? unitType = null)
	{
		if (NumSquadMembers == 0) // TODO pull default values from upgrade state
			NumSquadMembers = 4;
			
		if (unitType != null)
			UnitType = unitType.Value;
		
		this.transform.position = location;	
		mUnitPrefab = mUnitPrefabs[this.UnitType];
		
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
	
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods
	///////////////////////////////////////////////////////////////////////////////////
	private GameObject mUnitPrefab;
	private GameObject mEnemyPrefab;
	private Squad mTargetSquad;
	private List<Unit> mSquadMembers;
	
	static Dictionary<UnitType, GameObject> mUnitPrefabs = null;
	
	// Squad members form concentric circles around the squad center
	// the member width is used to determine the width of each band of the circles
	// ie. inner circle radius = kSquadMemberWidth * 1.5
	//     2nd circle width = kSquadMemberWidth * 1.5
	private const float kSquadMemberWidth = 3.0f;
	
	private void AttackTarget(Unit who, Target enemyUnit)
	{
		if (who == null || enemyUnit == null) 
			Debug.Break();
		
		IsEngaged = true;
		Unit u = (Unit) enemyUnit.GetComponent(typeof(Unit));
		mTargetSquad = u.Squad;
		AttackEnemySquad(who, u);
	}
	
	private void UpdateTarget(Unit who)
	{
		List<Unit> mEnemies = mTargetSquad.mSquadMembers;
		int numEnemies = mEnemies.Count;
		
		if (numEnemies == 0)
			DisengageSquad();
		else 
			who.Engage(mEnemies[Random.Range (0, numEnemies)]); // engage random enemy
	}
	
	private void ChangeSquadWeapons(Unit who, int weaponIndex)
	{
		if (mTargetSquad.mSquadMembers.Count <= 0)
			return;
		
		Debug.Log ("Weapon changed!");
		
		for(int i = 0; i < mSquadMembers.Count; ++i) 
			mSquadMembers[i].SwitchToWeapon(weaponIndex);
		
		// TODO replace with squad center or unit agnostic matching algorithm
		AttackEnemySquad(who, mTargetSquad.mSquadMembers[0]);
	}
	
	private void UpdateSquadMembers(Unit who)
	{
		mSquadMembers.Remove(who);
		NumSquadMembers --;
		
		if (NumSquadMembers <= 0) {
			Destroy(this.gameObject);
		}
	}
	
	private void AttackEnemySquad(Unit who, Unit enemyUnit)
	{
		// Surround the enemy!
		List<Vector3> positions = SurroundingPositions(
			enemyUnit.transform.position, 
			who.transform.position, 
			NumSquadMembers, 
			kSquadMemberWidth * 2.0f, 
			who.Range);
		
		List<Unit> mEnemies = mTargetSquad.mSquadMembers;
		int numEnemies = mEnemies.Count;
		
		// engage enemies 1 to 1
		for(int i = 0; i < mSquadMembers.Count; ++i) {
			mSquadMembers[i].collider2D.enabled = false;
			mSquadMembers[i].Engage(mEnemies[i % numEnemies], positions[i]);
		}
	}
	
	// re-enable the search for new enemies
	private void DisengageSquad()
	{
		IsEngaged = false;
		for(int i = 0; i < mSquadMembers.Count; ++i) {
			mSquadMembers[i].Disengage();
		}
	}
	
	/**
	Creates random directions for squad members to form a concentric circle around the target location
	
	@param numSections: how many sections to divide the circle into
	@param unitWidth: the width of each unit
	
	TODO fix the placement for large numbers of squad members
	**/
	private List<Vector3> RandomSectionLocations(int numSections, float circleWidth)
	{
		if (numSections <= 0) // return empty list
			return new List<Vector3>();
			
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
	
	public static void InitializePrefabs()
	{
		mUnitPrefabs = new Dictionary<UnitType, GameObject>();
		mUnitPrefabs.Add (UnitType.kSwordsman, Resources.Load ("Units/SwordsmanPrefab") as GameObject);
		mUnitPrefabs.Add (UnitType.kArcher, Resources.Load ("Units/ArcherPrefab") as GameObject);
		mUnitPrefabs.Add (UnitType.kPeasant, Resources.Load ("Units/PeasantPrefab") as GameObject);
		mUnitPrefabs.Add (UnitType.kMage, Resources.Load ("Units/MagePrefab") as GameObject);
	}
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	///////////////////////////////////////////////////////////////////////////////////
	
	// Use this for initialization
	void Awake () 
	{
		if (null == mUnitPrefabs) 
			InitializePrefabs();
			
		// only capture input in squad testing scene 
		if (!Application.loadedLevelName.Equals("SquadTest"))
			return;
	}
}
