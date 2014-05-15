using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SquadAction {
	EnemySighted, 
	WeaponChanged, 
	UnitDestroyed,
	DestinationReached,
	UnitDied,
	TargetDestroyed,
}

public enum UnitType {
	None,
	Peasant,
	Swordsman,
	Archer,
	Mage,
	King,
}

public enum SquadState {
	Idle,
	Moving,
	Engaging,
	Melee,
}

public class Squad : Target
{
	///////////////////////////////////////////////////////////////////////////////////
	// Public Methods
	///////////////////////////////////////////////////////////////////////////////////
	public int NumSquadMembers = 0;
    public List<Unit> SquadMembers { get; set; }
	public UnitType unitType;
	public Vector3 squadCenter;
	public Vector3 rallyPoint;
	public SquadState SquadState;
	public bool IsIdle { get { return SquadState == SquadState.Idle; } }
	public bool isIndependent = false;
	public SpriteRenderer sightCircle;
	
	public void Notify(SquadAction action, params object[] args)
	{
		switch (action) {
		case(SquadAction.EnemySighted):
			AttackTarget((Target)args[0]);
			break;
			
		case(SquadAction.DestinationReached):
			CheckSquadIdle();
			break;
			
		case(SquadAction.UnitDestroyed):
			AssignNewTarget((Unit)args[0]);
			break;
			
		case(SquadAction.WeaponChanged):
			ChangeSquadWeapons((Unit)args[0], (int)args[1]);
			break;
			
		case (SquadAction.UnitDied):
			UpdateSquadMembers((Unit)args[0]);
			break;
			
		case (SquadAction.TargetDestroyed):
			DisengageSquad();
			break;
		}
	}

	private void CheckSquadIdle()
	{
		int idleCount = 0;
		foreach (Unit u in squadMembers)
			if (u.IsIdle)
				idleCount ++;

		if ((float) idleCount / (float) squadMembers.Count > 0.75)
			this.SquadState = SquadState.Idle;	
	}

	public void UpdateSquadDestination(Vector3 location) 
	{
		this.SquadState = SquadState.Moving;
		rallyPoint = location;
		location.z = 0;
		
		// Randomize the squad's new position around the central location
		List<Vector3> randomPositions = 
			RandomSectionLocations(NumSquadMembers, kSquadMemberWidth * 1.5f);
		
		// Move each squad member to their new location
		for (int i = 0; i < NumSquadMembers; ++i) {
			Vector3 memberPosition = rallyPoint;
			memberPosition += randomPositions[i];
			squadMembers[i].MoveTo(memberPosition);
		}
	}
	
	public void Spawn(Vector3 location, UnitType? type = null, Allegiance allegiance = Allegiance.Rodelle)
	{
		if (type != null)
			unitType = type.Value;

		mAllegiance = allegiance;

		if (NumSquadMembers == 0)
			NumSquadMembers = GameState.UnitSquadCount[unitType];
		
		this.transform.position = location;	
		unitPrefab = unitPrefabs[this.unitType];
		
		if (squadMembers != null) {
			foreach (Unit u in squadMembers)
				Destroy(u.gameObject);
		}
		
		squadMembers = new List<Unit>();
		
		List<Vector3> randomPositions = this.RandomSectionLocations(NumSquadMembers, kSquadMemberWidth * 1.5f);
		
		// Instantiates and initializes the position of each member in the squad
		// TODO fix the placement for large numbers of squad members
		for (int i = 0; i < NumSquadMembers; ++i) {
			// instantiate the unit from the prefab
			GameObject o = (GameObject) Instantiate(unitPrefab);
			Unit u = (Unit) o.GetComponent(typeof(Unit));
			u.Squad = this;
			squadMembers.Add (u);
			
			// offset from squad center
			Vector3 memberPosition = this.transform.position;
			memberPosition += randomPositions[i];
			u.transform.position = memberPosition;
			u.Allegiance = this.Allegiance;
			this.GetComponent<CircleCollider2D>().radius = u.sightRange;
		}
		
		this.UpdateSquadDestination(this.rallyPoint);
	}
	
	public override void Damage(int damage) {}
	
	///////////////////////////////////////////////////////////////////////////////////
	// Private Methods
	///////////////////////////////////////////////////////////////////////////////////
	private GameObject unitPrefab;
	private GameObject enemyPrefab;
	private Target target;
	private List<Unit> squadMembers;
	static Dictionary<UnitType, GameObject> unitPrefabs = null;
		
	// Squad members form concentric circles around the squad center
	// the member width is used to determine the width of each band of the circles
	// ie. inner circle radius = kSquadMemberWidth * 1.5
	//     2nd circle width = kSquadMemberWidth * 1.5
	private const float kSquadMemberWidth = 3.0f;
	
	private void AttackTarget(Target enemyUnit)
	{
		if (enemyUnit == null) 
			Debug.Break();
		
		Unit u = (Unit) enemyUnit.GetComponent(typeof(Unit));
		if (u != null) {
			target = u.Squad;
			AttackEnemySquad(u.Squad);
			return;
		}
		
		Tower t = (Tower) enemyUnit.GetComponent(typeof(Tower));
		if (t != null) {
			target = t;
			AttackEnemyTower(t);
			return;
		}
	}
		
	private void AssignNewTarget(Unit who)
	{
		GameState.AddExperience(this.unitType, 1);
		List<Unit> mEnemies = ((Squad)target).squadMembers;
		int numEnemies = mEnemies.Count;
		
		if (numEnemies == 0)
			DisengageSquad();
		else 
			who.Engage(mEnemies[Random.Range (0, numEnemies)]); // engage random enemy
	}
	
	private void ChangeSquadWeapons(Unit who, int weaponIndex)
	{
		if (target == null)
			return;
		
		for(int i = 0; i < squadMembers.Count; ++i) 
			squadMembers[i].SwitchToWeapon(weaponIndex);
		
		AttackTarget(target);
	}
	
	private void UpdateSquadMembers(Unit who)
	{
		if (this.gameObject == null) // game object m
			return;
			
		squadMembers.Remove(who);
		NumSquadMembers --;
		
		if (squadMembers.Count <= 0) {
			Destroy (this.gameObject);
		}
	}
	
	private void AttackEnemySquad(Squad enemySquad)
	{
		this.SquadState = SquadState.Engaging;
		
		Unit squadUnit = this.squadMembers[0];
		// Surround the enemy!
		List<Vector3> positions = SurroundingPositions(
			enemySquad.squadCenter,
			this.squadCenter, 
			squadMembers.Count, 
			kSquadMemberWidth * 2.0f, 
			squadUnit.Range); // TODO add target radius for large targets
		
		List<Unit> mEnemies = enemySquad.squadMembers;
		int numEnemies = mEnemies.Count;
		
		// engage enemies 1 to 1
		for(int i = 0; i < squadMembers.Count; ++i) {
			squadMembers[i].Engage(mEnemies[i % numEnemies], positions[i]);
		}
		
		if (enemySquad.SquadState == SquadState.Idle || enemySquad.SquadState == SquadState.Moving)
			enemySquad.Notify(SquadAction.EnemySighted, this.squadMembers[0]);
	}
	
	private void AttackEnemyTower(Tower tower)
	{
		this.SquadState = SquadState.Engaging;
		
		Unit squadUnit = this.squadMembers[0];
		// Surround the enemy!
		List<Vector3> positions = SurroundingPositions(
			tower.Position,
			this.squadCenter, 
			NumSquadMembers, 
			kSquadMemberWidth * 2.0f, 
			squadUnit.Range); // TODO add target radius for large targets
			
		// engage in a circle around the tower
		for(int i = 0; i < squadMembers.Count; ++i) {
			squadMembers[i].Engage(tower, positions[i]);
		}
	}
	
	// re-enable the search for new enemies
	private void DisengageSquad()
	{
		this.SquadState = SquadState.Moving;
		for(int i = 0; i < squadMembers.Count; ++i) {
			squadMembers[i].Disengage();
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
		unitPrefabs = new Dictionary<UnitType, GameObject>();
		unitPrefabs.Add (UnitType.Swordsman, Resources.Load ("Units/SwordsmanPrefab") as GameObject);
		unitPrefabs.Add (UnitType.Archer, Resources.Load ("Units/ArcherPrefab") as GameObject);
		unitPrefabs.Add (UnitType.Peasant, Resources.Load ("Units/PeasantPrefab") as GameObject);
		unitPrefabs.Add (UnitType.Mage, Resources.Load ("Units/MagePrefab") as GameObject);
		unitPrefabs.Add (UnitType.King, Resources.Load ("Units/KingPrefab") as GameObject);
	}
	
	///////////////////////////////////////////////////////////////////////////////////
	// Unity Overrides
	///////////////////////////////////////////////////////////////////////////////////
	
	void OnTriggerStay2D(Collider2D other)
	{
		this.OnTriggerEnter2D(other);
	}
	
	// Check for enemies in sight range
	void OnTriggerEnter2D(Collider2D other)
	{
		if (this.SquadState != SquadState.Idle && this.SquadState != SquadState.Moving)
			return;
		
		Target target = other.gameObject.GetComponent<Target>();
		
		if (target is Squad) // do not target squads directly
			return;
		
		if (target != null && target.Allegiance != this.Allegiance) {
			Notify (SquadAction.EnemySighted, target);
		}
		//this.OnTriggerEnter2D(other);
	}
		
	void FixedUpdate()
	{
		if (squadMembers == null)
			return;
		
		if (this.SquadState == SquadState.Engaging && squadMembers[0].Range <= 12f) // melee range
			this.SquadState = SquadState.Melee;
		
		float xTotal = 0;
		float yTotal = 0;
		float unitCount = (float) squadMembers.Count;
		foreach (Unit unit in squadMembers) {
			xTotal += unit.transform.position.x;
			yTotal += unit.transform.position.y;
		}
		
		// average central location
		this.squadCenter = new Vector3(xTotal / unitCount, yTotal / unitCount, 0f);
		this.transform.position = squadCenter;
	}
	
	// Use this for initialization
	void Awake () 
	{
		if (null == unitPrefabs) 
			InitializePrefabs();
	
		
		SquadState = SquadState.Idle;
		if (isIndependent) {
			this.Spawn(this.Position);
			this.UpdateSquadDestination(this.Position);
		}
	}
}
