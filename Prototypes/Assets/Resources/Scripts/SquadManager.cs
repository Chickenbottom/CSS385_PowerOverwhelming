using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadManager : MonoBehaviour {

	private List<SquadBehavior> mSquads;
	GameObject mSquadPrefab = null;
	
	private float kSquadWidth = 10f;
	
	void Start () 
	{
		if (null == mSquadPrefab) 
			mSquadPrefab = Resources.Load("Prefabs/Squad") as GameObject;
		mSquads = new List<SquadBehavior>();
	}
	
	void Update () 
	{
		if (Input.GetButtonDown("Fire1")) {
			GameObject o = (GameObject) Instantiate(mSquadPrefab);
			SquadBehavior u = (SquadBehavior) o.GetComponent(typeof(SquadBehavior));
			mSquads.Add (u);
			
			u.MoveSquad(new Vector3(Random.Range (-50f, 50f), Random.Range(-50f, 50f), 0f));
		}
		
		// Move the squads to the location clicked on the screen
		if (Input.GetMouseButtonDown(0)) {
			MoveSquads(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}
	
	private void MoveSquads(Vector3 location)
	{
		location.z = 0;
		
		List<Vector3> randomPositions = this.RandomSectionLocations(mSquads.Count, kSquadWidth);
		

		this.transform.position = location;
		
		for (int i = 0; i < mSquads.Count; ++i) {
			Vector3 moveTo = location;
			moveTo += randomPositions[i];
			mSquads[i].MoveSquad(moveTo);
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