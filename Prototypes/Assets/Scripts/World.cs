using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public float Width { get {return mWorldMax.x; } }
	public float Height { get {return mWorldMax.y; } }
	private Bounds mWorldBound;  // this is the world bound
	private Vector2 mWorldMin;	// Better support 2D interactions
	private Vector2 mWorldMax;
	private Vector2 mWorldCenter;
	private Camera mMainCamera;
	
	
	// Use this for initialization
	void Start () 
	{
		mMainCamera = Camera.main;
		mWorldBound = new Bounds(Vector3.zero, Vector3.one);
		UpdateWorldWindowBound();
	}
	
	void Update () 
	{
	
	} 
	
	#region Game Window World size bound support
	public enum WorldBoundStatus {
		CollideTop,
		CollideLeft,
		CollideRight,
		CollideBottom,
		Outside,
		Inside
	};
	
	/// <summary>
	/// This function must be called anytime the MainCamera is moved, or changed in size
	/// </summary>
	public void UpdateWorldWindowBound()
	{
		// get the main 
		if (null != mMainCamera) {
			float maxY = mMainCamera.orthographicSize;
			float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
			float sizeX = 2 * maxX;
			float sizeY = 2 * maxY;
			float sizeZ = Mathf.Abs(mMainCamera.farClipPlane - mMainCamera.nearClipPlane);
			
			// Make sure z-component is always zero
			Vector3 c = mMainCamera.transform.position;
			c.z = 0.0f;
			mWorldBound.center = c;
			mWorldBound.size = new Vector3(sizeX, sizeY, sizeZ);
			
			mWorldCenter = new Vector2(c.x, c.y);
			mWorldMin = new Vector2(mWorldBound.min.x, mWorldBound.min.y);
			mWorldMax = new Vector2(mWorldBound.max.x, mWorldBound.max.y);
		}
	}
	
	public Vector2 WorldCenter { get { return mWorldCenter; } }
	public Vector2 WorldMin { get { return mWorldMin; }} 
	public Vector2 WorldMax { get { return mWorldMax; }}
	
	public WorldBoundStatus ObjectCollideWorldBound(Vector3 objPos, Collider2D objCollider, out float overlap)
	{
		WorldBoundStatus status = WorldBoundStatus.Inside;
		overlap = 0;
		Bounds objBound = new Bounds();
		
		CircleCollider2D objCircleCollider = (null != objCollider) ? (objCollider as CircleCollider2D) : null;
		
		if (null != objCircleCollider) {
			Vector3 center = objPos + new Vector3 (objCircleCollider.center.x, objCircleCollider.center.y, 0f);
			float size = objCircleCollider.radius * 2f; // this is the actual size of the circle
			
			Vector3 bSize = new Vector3 (size, size, size);
			objBound = new Bounds (center, bSize);
		} 
		
		BoxCollider2D objBoxCollider = 
			(objCollider != null && objCircleCollider == null) 
				? (objCollider as BoxCollider2D) 
				: null;
		
		if (null != objBoxCollider) {
			//Debug.Log ("Box Collision");
			Vector3 center = objPos + new Vector3 (objBoxCollider.center.x, objBoxCollider.center.y, 0f);
			float width = objBoxCollider.size.x; // this is the actual size of the circle
			float height = objBoxCollider.size.y; // this is the actual size of the circle
			
			Vector3 bSize = new Vector3 (width, height, 0);
			objBound = new Bounds (center, bSize);
		} 
		
		
		if (objCircleCollider != null || objBoxCollider != null) {
			if (mWorldBound.Intersects (objBound)) {
				if (objBound.max.x > mWorldBound.max.x) {
					status = WorldBoundStatus.CollideRight;
					overlap = mWorldBound.max.x - objBound.max.x;
				} else if (objBound.min.x < mWorldBound.min.x) {
					status = WorldBoundStatus.CollideLeft;
					overlap = mWorldBound.min.x - objBound.min.x;
					
					
				} else if (objBound.max.y > mWorldBound.max.y) {
					status = WorldBoundStatus.CollideTop;
					overlap = mWorldBound.max.y - objBound.max.y;
				} else if (objBound.min.y < mWorldBound.min.y) {
					status = WorldBoundStatus.CollideBottom;
					overlap = mWorldBound.min.y - objBound.min.y;
				} else if ((objBound.min.z < mWorldBound.min.z) || (objBound.max.z > mWorldBound.max.z)) {
					status = WorldBoundStatus.Outside;
				}
			} else 
				status = WorldBoundStatus.Outside;
		} else {
			Debug.Log ("Need to put a CircleCollider2D or BoxCollider2D on the object to call ObjectColideWorldBound()!");
		}
		return status;
		
	}
	#endregion 
}
