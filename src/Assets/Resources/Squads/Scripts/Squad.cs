using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SquadAction
{
    EnemySighted, 
    EngagedInMelee, 
    UnitDestroyed,
    DestinationReached,
    UnitDied,
    TargetDestroyed,
}

public enum SquadState
{
    Idle,
    Moving,
    ForcedMove,
    Engaging,
    Melee,
}

public class Squad : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Public Methods
    ///////////////////////////////////////////////////////////////////////////////////
    public int NumSquadMembers = 0;
    public UnitType UnitType;
    public Vector3 SquadCenter;
    public Vector3 RallyPoint;

    public Unit SquadLeader { get { return mSquadMembers [0]; } }

    public SquadState SquadState { 
        get { return mSquadState; } 
        set { mSquadState = value; }
    }

    public bool IsIdle { get { return SquadState == SquadState.Idle; } }

    public bool IsEngaged { 
        get { 
            return ! (SquadState == SquadState.Idle || 
                SquadState == SquadState.Moving); 
        } 
    }
    
    public bool IsIndependent = false;
    public SpriteRenderer SightCircle;
    
    public void Notify (SquadAction action, params object[] args)
    {
        switch (action) {
        case(SquadAction.EnemySighted):
            AttackTarget ((Target)args [0]);
            break;
            
        case(SquadAction.DestinationReached):
            CheckSquadIdle ();
            break;
            
        case(SquadAction.UnitDestroyed):
            AssignNewTarget ((Unit)args [0]);
            break;
            
        case(SquadAction.EngagedInMelee):
            EngageInMelee ();
            break;
            
        case (SquadAction.UnitDied):
            UpdateSquadMembers ((Unit)args [0]);
            break;
            
        case (SquadAction.TargetDestroyed):
            Disengage ();
            break;
        }
    }

    private void CheckSquadIdle ()
    {
        int idleCount = 0;
        foreach (Unit u in mSquadMembers)
            if (u.IsIdle)
                idleCount ++;

        if ((float)idleCount / (float)mSquadMembers.Count > 0.25)
            this.SquadState = SquadState.Idle;
    }
    
    public void ForceMove (Vector3 location)
    {
        Disengage ();
        this.SquadState = SquadState.ForcedMove;
        RallyPoint = location;
        location.z = 0;
        
        UpdateSquadCoherency ();
    }
    
    public void SetDestination (Vector3 location)
    {
        this.SquadState = SquadState.Moving;
        RallyPoint = location;
        location.z = 0;
        
        UpdateSquadCoherency ();
    }
    
    public void Spawn (Vector3 location, UnitType? type = null, Allegiance allegiance = Allegiance.Rodelle)
    {
        if (type != null)
            UnitType = type.Value;

        mAllegiance = allegiance;

        if (NumSquadMembers == 0)
            NumSquadMembers = GameState.UnitSquadCount [UnitType];
        
        this.transform.position = location; 
        
        if (allegiance == Allegiance.Rodelle)
            mUnitPrefab = mUnitPrefabs [this.UnitType];
        else 
            mUnitPrefab = mEnemyPrefabs [this.UnitType];
        
        if (mSquadMembers != null) {
            foreach (Unit u in mSquadMembers)
                Destroy (u.gameObject);
        }
        
        mSquadMembers = new List<Unit> ();
        
        List<Vector3> randomPositions = this.RandomSectionLocations (NumSquadMembers, kSquadMemberWidth * 1.5f);
        
        // Instantiates and initializes the position of each member in the squad
        // TODO fix the placement for large numbers of squad members
        for (int i = 0; i < NumSquadMembers; ++i) {
            // instantiate the unit from the prefab
            GameObject o = (GameObject)Instantiate (mUnitPrefab);
            Unit u = (Unit)o.GetComponent (typeof(Unit));
            u.Squad = this;
            mSquadMembers.Add (u);
            
            // offset from squad center
            Vector3 memberPosition = this.transform.position;
            memberPosition += randomPositions [i];
            u.transform.position = memberPosition;
            u.Allegiance = mAllegiance;
            this.GetComponent<CircleCollider2D> ().radius = u.SightRange;
        }
        
        this.SetDestination (this.RallyPoint);
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private Methods
    ///////////////////////////////////////////////////////////////////////////////////
    private GameObject mUnitPrefab;
    private GameObject mEnemyPrefab;
    private Squad mTargetSquad;
    private List<Unit> mSquadMembers;
    protected Allegiance mAllegiance;
    private SquadState mSquadState;
        
    // Squad members form concentric circles around the squad center
    // the member width is used to determine the width of each band of the circles
    // ie. inner circle radius = kSquadMemberWidth * 1.5
    //     2nd circle width = kSquadMemberWidth * 1.5
    private const float kSquadMemberWidth = 3.0f;
    
    private void AttackTarget (Target target)
    {
        if (!(mTargetSquad == null || mTargetSquad.UnitType == UnitType.King))
            return;
            
        if (target == null) 
            Debug.Break ();
        
        if (target is Unit) {
            Unit u = (Unit)target.GetComponent (typeof(Unit));
            mTargetSquad = u.Squad;
            AttackEnemySquad (u.Squad);
        } else if (target is Tower) {
            Tower t = (Tower)target.GetComponent (typeof(Tower));
            AttackEnemyTower (t);
        }
    }
        
    private void AssignNewTarget (Unit who)
    {
        GameState.Gold += 5;
        
        if (this.mAllegiance == Allegiance.Rodelle)
            UnitUpgrades.AddToExperience (this.UnitType, 1);
            
        List<Unit> mEnemies = mTargetSquad.mSquadMembers;
        int numEnemies = mEnemies.Count;
        
        if (numEnemies == 0)
            Disengage ();
        else 
            who.Engage (mEnemies [Random.Range (0, numEnemies)]); // engage random enemy
    }
    
    private void UpdateSquadMembers (Unit who)
    {
        if (this.gameObject == null)
            return;
            
        mSquadMembers.Remove (who);
        NumSquadMembers --;
        
        if (mSquadMembers.Count <= 0) {
            Destroy (this.gameObject);
        }
    }
    
    private void AttackEnemySquad (Squad enemySquad)
    {
        if (enemySquad == null)
            return;
            
        this.SquadState = SquadState.Engaging;
        
        Unit squadUnit = this.SquadLeader;
        // Surround the enemy!
        List<Vector3> positions = SurroundingPositions (
            enemySquad.SquadCenter,
            this.SquadCenter, 
            mSquadMembers.Count, 
            kSquadMemberWidth * 2.0f, 
            squadUnit.Range); // TODO add target radius for large targets
        
        List<Unit> mEnemies = enemySquad.mSquadMembers;
        int numEnemies = mEnemies.Count;
        
        // engage enemies 1 to 1
        for (int i = 0; i < mSquadMembers.Count; ++i) {
            mSquadMembers [i].Engage (mEnemies [i % numEnemies], positions [i]);
        }
        
        enemySquad.Notify (SquadAction.EnemySighted, this.SquadLeader);
    }
    
    private void AttackEnemyTower (Tower tower)
    {
        this.SquadState = SquadState.Engaging;
        
        Unit squadUnit = this.SquadLeader;
        // Surround the enemy!
        List<Vector3> positions = SurroundingPositions (
            tower.Position,
            this.SquadCenter, 
            mSquadMembers.Count, 
            kSquadMemberWidth * 2.0f, 
            squadUnit.Range); // TODO add target radius for large targets
            
        // engage in a circle around the tower
        for (int i = 0; i < mSquadMembers.Count; ++i) {
            mSquadMembers [i].Engage (tower, positions [i]);
        }
    }
    
    // re-enable the search for new enemies
    private void Disengage ()
    {
        mTargetSquad = null;
        this.SquadState = SquadState.Moving;
        for (int i = 0; i < mSquadMembers.Count; ++i) {
            mSquadMembers [i].Disengage ();
        }
    }
    
    public void EngageInMelee ()
    {
        this.SquadState = SquadState.Melee;
        for (int i = 0; i < mSquadMembers.Count; ++i)
            mSquadMembers [i].SwitchToMeleeWeapon ();
    }
    
    private void UpdateSquadCoherency ()
    {
        // Randomize the squad's new position around the central location
        List<Vector3> randomPositions = 
            RandomSectionLocations (NumSquadMembers, kSquadMemberWidth * 1.5f);
        
        // Move each squad member to their new location
        for (int i = 0; i < NumSquadMembers; ++i) {
            Vector3 memberPosition = RallyPoint;
            memberPosition += randomPositions [i];
            mSquadMembers [i].MoveTo (memberPosition);
        }
    }
    
    /**
    Creates random directions for squad members to form a concentric circle around the target location
    
    @param numSections: how many sections to divide the circle into
    @param unitWidth: the width of each unit
    
    TODO fix the placement for large numbers of squad members
    **/
    private List<Vector3> RandomSectionLocations (int numSections, float circleWidth)
    {
        if (numSections <= 0) // return empty list
            return new List<Vector3> ();
            
        List<Vector3> randomLocations = new List<Vector3> ();
        
        List<float> randomAngles = new List<float> ();
        float anglesPerSection = 360f / (numSections - 1);
        for (int i = 0; i < numSections; ++i) {
            float randomAngle = Random.Range (anglesPerSection * .3f, anglesPerSection * 0.7f);
            randomAngle += i * anglesPerSection;
            randomAngles.Add (randomAngle * Mathf.Deg2Rad);
        }
        
        randomLocations.Add (Vector3.zero); // place first squad in center
        // assigns a random position in the section
        for (int i = 1; i < numSections; ++i) {
            float angle = randomAngles [i];
            Vector3 randomDir = new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0f);
            randomDir.Normalize ();
            //randomDir *= circleWidth;
            randomDir *= Random.Range (circleWidth, circleWidth * 2f);
            randomLocations.Add (randomDir);
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
    private List<Vector3> SurroundingPositions (Vector3 x, Vector3 y, int n, float w, float r)
    {
        List<Vector3> surroundingPositions = new List<Vector3> ();
        
        Vector3 toTarget = y - x;
        toTarget.Normalize ();
        float sign = Vector3.Cross (Vector3.right, toTarget).normalized.z;
        float theta = Mathf.Acos (Vector3.Dot (Vector3.right, toTarget));
        theta *= Mathf.Rad2Deg * sign;
        
        float angleBetweenUnits = 2f * Mathf.Atan (0.5f * w / r) * Mathf.Rad2Deg;
        
        List<float> angles = new List<float> ();
        
        float toggle = 1;
        for (int i = 0; i < n; ++i) {
            toggle *= -1; // flip back and forth
            theta += i * toggle * angleBetweenUnits;
            
            angles.Add (theta * Mathf.Deg2Rad);
        }
        
        for (int i = 0; i < n; ++i) {
            Vector3 newPosition = x;
            newPosition.x = r * Mathf.Cos (angles [i]);
            newPosition.y = r * Mathf.Sin (angles [i]);
            
            surroundingPositions.Add (newPosition);
        }
        
        return surroundingPositions;
    }
    
    static Dictionary<UnitType, GameObject> mUnitPrefabs = null;
    static Dictionary<UnitType, GameObject> mEnemyPrefabs = null;
    
    public static void InitializePrefabs ()
    {
        mUnitPrefabs = new Dictionary<UnitType, GameObject> ();
        mUnitPrefabs.Add (UnitType.Swordsman, Resources.Load ("Units/SwordsmanPrefab") as GameObject);
        mUnitPrefabs.Add (UnitType.Archer, Resources.Load ("Units/ArcherPrefab") as GameObject);
        mUnitPrefabs.Add (UnitType.Peasant, Resources.Load ("Units/PeasantPrefab") as GameObject);
        mUnitPrefabs.Add (UnitType.Mage, Resources.Load ("Units/MagePrefab") as GameObject);
        mUnitPrefabs.Add (UnitType.King, Resources.Load ("Units/KingPrefab") as GameObject);
        
        mEnemyPrefabs = new Dictionary<UnitType, GameObject> ();
        mEnemyPrefabs.Add (UnitType.Swordsman, Resources.Load ("Units/EnemySwordsmanPrefab") as GameObject);
        mEnemyPrefabs.Add (UnitType.Archer, Resources.Load ("Units/EnemyArcherPrefab") as GameObject);
        mEnemyPrefabs.Add (UnitType.Peasant, Resources.Load ("Units/PeasantPrefab") as GameObject);
        mEnemyPrefabs.Add (UnitType.Mage, Resources.Load ("Units/EnemyMagePrefab") as GameObject);
    }
        
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////
    
    void OnTriggerStay2D (Collider2D other)
    {
        this.OnTriggerEnter2D (other);
    }
    
    // Check for enemies in sight range
    void OnTriggerEnter2D (Collider2D other)
    {
        if (this.IsEngaged)
            return;
        
        Target target = other.gameObject.GetComponent<Target> ();
        
        if (target != null && target.Allegiance != mAllegiance) {
            Notify (SquadAction.EnemySighted, target);
        }
        //this.OnTriggerEnter2D(other);
    }
        
    void FixedUpdate ()
    {
        if (mSquadMembers == null)
            return;
                
        float xTotal = 0;
        float yTotal = 0;
        float unitCount = (float)mSquadMembers.Count;
        foreach (Unit unit in mSquadMembers) {
            xTotal += unit.transform.position.x;
            yTotal += unit.transform.position.y;
        }
        
        // average central location
        this.SquadCenter = new Vector3 (xTotal / unitCount, yTotal / unitCount, 0f);
        this.transform.position = SquadCenter;
    }
    
    // Use this for initialization
    void Awake ()
    {
        if (null == mUnitPrefabs) 
            InitializePrefabs ();
    
        this.SquadState = SquadState.Idle;
        if (IsIndependent) {
            this.Spawn (this.transform.position);
            this.SetDestination (this.transform.position);
        }
    }
}
