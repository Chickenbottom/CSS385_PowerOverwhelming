using UnityEngine;
using System.Collections;

public class IceBomb : MonoBehaviour {
	public GameObject Emitter;
	
	// TODO replace with states
	bool mExplosionDone = false;
	
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
			mExplosionDone = true;
	}
	
	void OnTriggerStay2D(Collider2D other) 
	{
		if (mExplosionDone)
			return;
			
		Target target = other.gameObject.GetComponent<Target>();
		FrozenEffect f = other.gameObject.GetComponent<FrozenEffect>();
		
		// do not target squads directly, do not retarget frozen targets
		if (target is Squad || f != null) 
			return;
		
		Squad s = ((Unit) mSource).Squad;
		if (target != null && target.Allegiance != s.Allegiance) {
			target.gameObject.AddComponent("FrozenEffect");
			f = other.gameObject.GetComponent<FrozenEffect>();
			f.unit = target.GetComponent<Unit>();
			f.Freeze();
			
			IDamagable e = (IDamagable) target.GetComponent(typeof(IDamagable));
			e.Damage(4);
		}
	
	}
	
	Target mTarget;
	Target mSource;	
	public void SetParameters(Target src, Target target)
	{
		mTarget = target;
		mSource = src;
		transform.position = target.Position;
		StartExplosion();
	}
	
	private void StartExplosion()
	{
		mEmitterPrefab = (GameObject) Instantiate(Emitter);
		mEmitterPrefab.transform.position = this.transform.position;
		kStartTime = Time.time;
	}
	
	
}
