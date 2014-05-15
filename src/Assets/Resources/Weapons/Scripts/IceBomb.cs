using UnityEngine;
using System.Collections;

public class IceBomb : MonoBehaviour {
	public GameObject emitter;
	
	// TODO replace with states
	bool explosionDone = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	float kLiveTimer = 1.8f;
	float kExplosionDuration = 0.1f;
	float kStartTime;
	GameObject mEmitterPrefab = null;
	
	void Update()
	{
		if (Time.time - kStartTime > kLiveTimer) {
			Destroy (mEmitterPrefab.gameObject);
			Destroy (this.gameObject);
		}
		
		if (Time.time - kStartTime > kExplosionDuration)
			explosionDone = true;
	}
	
	void OnTriggerStay2D(Collider2D other) 
	{
		if (explosionDone)
			return;
			
		Target target = other.gameObject.GetComponent<Target>();
		FrozenEffect f = other.gameObject.GetComponent<FrozenEffect>();
		
		// do not retarget frozen targets
		if (f != null) 
			return;

		if (target is Tower) {
			target.Damage(4);
			return;
		}

		if (! (target is Unit))
			return;

		if (target != null && target.Allegiance != mSource.Allegiance) {
			target.gameObject.AddComponent("FrozenEffect");
			f = other.gameObject.GetComponent<FrozenEffect>();
			f.target = target.GetComponent<Target>();
			f.Freeze();
			
			target.Damage(5);
		}
	
	}
	
	Target mTarget;
	Target mSource;	
	public void SetParameters(Target src, Target target)
	{
		//mTarget = target;
		mSource = src;
		transform.position = target.Position;
		StartExplosion();
	}
	
	private void StartExplosion()
	{
		mEmitterPrefab = (GameObject) Instantiate(emitter);
		mEmitterPrefab.transform.position = this.transform.position;
		kStartTime = Time.time;
	}
	
	
}
