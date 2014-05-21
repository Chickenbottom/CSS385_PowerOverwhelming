using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadManager : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Public Methods and Variables
    ///////////////////////////////////////////////////////////////////////////////////
    public UnitType squadType = UnitType.None;
    public Tower ControllingTower;
    
    public Sprite DefaultSprite;
    public Sprite GlowingSprite;
    
    public void SetDestination (Vector3 location)
    {
        location.z = 0;
        RemoveDeadSquads ();
        rallyPoint = location;
        
        if (Vector3.Distance (this.transform.position, location) < 5f) 
            ForceMove (location);
        else 
            MoveTo (location);
    
        this.transform.position = location;
    }
    
    public void AddSquad (Squad squad)
    {
        squads.Add (squad);
        this.SetDestination (rallyPoint);
    }
    
    public void AddSquad (Vector3 spawnLocation, UnitType unitType = UnitType.None)
    {
        // nothing to instantiate
        if (unitType == UnitType.None && this.squadType == UnitType.None)
            Debug.LogError ("SquadManager - spawn type not specified.");
            
        Squad squad;
        if (unitType == UnitType.None)
            squad = SpawnSquadFromUnitType (spawnLocation, this.squadType);
        else
            squad = SpawnSquadFromUnitType (spawnLocation, unitType);
            
        squads.Add (squad);
        squad.SetDestination (rallyPoint);
    }
    
    public int NumSquads ()
    {
        this.RemoveDeadSquads ();
        return squads.Count;
    }
    
    public void Glow()
    {
        this.GetComponent<SpriteRenderer>().sprite = GlowingSprite;
        Invoke ("DisableGlow", 2f);
    }
    
    public void DisableGlow()
    {
        this.GetComponent<SpriteRenderer>().sprite = DefaultSprite;
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private Methods and Variables
    ///////////////////////////////////////////////////////////////////////////////////
    
    private List<Squad> squads;
    private float squadWidth = 8f; // TODO replace with function calculating width of squad
    private Vector3 rallyPoint;
    private static GameObject squadPrefab = null;
    
    //float mDoubleClickStart = 0;
    
    private Squad SpawnSquadFromUnitType (Vector3 location, UnitType unitType)
    {
        GameObject o = (GameObject)Instantiate (squadPrefab);
        Squad squad = (Squad)o.GetComponent (typeof(Squad));
        squad.UnitType = unitType;
        squad.Spawn (location, unitType, Allegiance.Rodelle);
        return squad;
    }
    
    // Creates random directions for squad members to form a concentric circle around the target location
    // TODO fix the placement for large numbers of squad members
    // TODO move this to a general utility class
    private List<Vector3> RandomSectionLocations (int numSections, float circleWidth)
    {
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
    
    private void ForceMove (Vector3 location)
    {
        List<Vector3> randomPositions = this.RandomSectionLocations (squads.Count, squadWidth);
        for (int i = 0; i < squads.Count; ++i) 
            squads [i].ForceMove (location + randomPositions [i]);
    }
    
    private void MoveTo (Vector3 location)
    {
        List<Vector3> randomPositions = this.RandomSectionLocations (squads.Count, squadWidth);
        for (int i = 0; i < squads.Count; ++i) 
            squads [i].SetDestination (location + randomPositions [i]);
    }
    
    private void RemoveDeadSquads ()
    {
        for (int i = squads.Count - 1; i >= 0; --i) {
            if (squads [i] == null) {
                squads.RemoveAt (i);
                continue;
            }
        }
    }
    
    //private void OnDoubleClick ()
    //{
    //    this.ForceMove(this.transform.position);
    //    this.Glow();
    //}
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////
    
    void Awake ()
    {
        if (squadPrefab == null)
            squadPrefab = Resources.Load ("Squads/SquadPrefab") as GameObject;
        
        squads = new List<Squad> ();
        rallyPoint = this.transform.position;
    }
    
    //void OnMouseDown()
    //{
    //    GameObject.Find ("GameManager").GetComponent<MouseManager> ().TowerClicked(ControllingTower);
    //}
    
    void OnMouseUpAsButton ()
    {
        //if ((Time.time - mDoubleClickStart) < 0.3f) {
        //    this.OnDoubleClick ();
        //    mDoubleClickStart = -1;
        //} else {
        //    mDoubleClickStart = Time.time;
        //}
        this.ForceMove(this.transform.position);
        this.Glow();
    }
}