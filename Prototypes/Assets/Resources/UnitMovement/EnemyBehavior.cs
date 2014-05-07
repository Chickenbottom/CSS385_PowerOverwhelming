using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	private Vector3 mTarget;
	private float mSpeed = 20f;
	
	
	private bool isAtTarget = false;
	
	void Start () {
		mTarget = new Vector3(80, 0, 0);
		
		Vector3 initialDir = mTarget;
		initialDir.Normalize();
		//this.transform.up = initialDir;
		mSpeed = Random.Range(mSpeed * .8f, mSpeed * 1.2f);
	}
	
	// Update is called once per frame
	void Update () {
		//RotateTowards(mTarget);
		if (isAtTarget)
			return;
		
		UpdateMovement(mSpeed);
		
		if (Vector3.Distance(this.transform.position, mTarget) < 1.0f)
			isAtTarget = true;
		
		//CheckCollision();
	}
	
	void OnCollisionEnter()
	{
		this.transform.up = new Vector3(0, 1, 0);
		Debug.Log ("Collision");
	}
	
	private void CheckCollision()
	{
		World world = GameObject.Find ("World").GetComponent<World>();
		
		float overlap;
		World.WorldBoundStatus status =
			world.ObjectCollideWorldBound(this.transform.position, this.collider2D, out overlap);
		
		if (status != World.WorldBoundStatus.Inside) {
			ChangeDirection();
		}	
		
		switch (status) 
		{
		case(World.WorldBoundStatus.CollideRight):
		case(World.WorldBoundStatus.CollideLeft):
			transform.Translate (overlap, 0, 0, Space.World);
			break;
		case(World.WorldBoundStatus.CollideTop):
		case(World.WorldBoundStatus.CollideBottom):
			transform.Translate(0, overlap, 0, Space.World);
			break;
		}
	}
	
	private void ChangeDirection()
	{
		Vector2 r = Random.insideUnitCircle;
		transform.up = new Vector3 (0, r.y, 0);
	}
	
	public void UpdateMovement(float speed)
	{
		Vector3 targetDir = mTarget - transform.position;
		targetDir.Normalize();
		
		speed *= Time.smoothDeltaTime;
		
		transform.position += speed * targetDir;
	}
}
