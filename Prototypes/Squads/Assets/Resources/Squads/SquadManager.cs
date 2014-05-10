using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadManager : MonoBehaviour {

	private List<Squad> mSquads;
	GameObject mSquadPrefab = null;
	
	private float kSquadWidth = 10f;
	
	public string SquadPrefab;
	
	private Vector3 mRallyPoint;
	
	void Start () 
	{
		if (null == mSquadPrefab) 
			mSquadPrefab = Resources.Load(SquadPrefab) as GameObject;
		mSquads = new List<Squad>();
		mRallyPoint = this.transform.position;
	}
	
	void Update () 
	{
		if (Input.GetButtonDown("Fire1")) {
			GameObject o = (GameObject) Instantiate(mSquadPrefab);
			Squad u = (Squad) o.GetComponent(typeof(Squad));
			mSquads.Add (u);
			MoveSquads(mRallyPoint);
		}
		
		// Move the squads to the location clicked on the screen
		if (Input.GetMouseButtonDown(0)) {
			mRallyPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mRallyPoint.z = 0;
			MoveSquads(mRallyPoint);
		}
	}
	
	private void MoveSquads(Vector3 location)
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
	
	// Creates random directions for squad members to form a concentric circle around the target location
	// TODO fix the placement for large numbers of squad members
	// TODO create location in center for odd number of members?
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
}