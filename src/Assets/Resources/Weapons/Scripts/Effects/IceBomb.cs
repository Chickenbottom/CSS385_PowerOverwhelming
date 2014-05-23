using UnityEngine;
using System.Collections;

public class IceBomb : MonoBehaviour
{
    public GameObject emitter;
    
    // TODO replace with states
    bool explosionDone = false;
    
    // Use this for initialization
    void Start ()
    {
        /*
        if (mTarget is Unit) {
            mTarget.gameObject.AddComponent ("FrozenEffect");
            FrozenEffect f = mTarget.gameObject.GetComponent<FrozenEffect> ();
            f.target = mTarget.GetComponent<Target> ();
            f.Freeze ();
        }
          */  
            
    }
    
    float kLiveTimer = 1.8f;
    float kExplosionDuration = 0.1f;
    float kStartTime;
    GameObject mEmitterPrefab = null;
    
    void Update ()
    {
        if (Time.time - kStartTime > kLiveTimer) {
            Destroy (mEmitterPrefab.gameObject);
            Destroy (this.gameObject);
        }
        
        if (Time.time - kStartTime > kExplosionDuration)
            explosionDone = true;
    }
    
   void OnTriggerEnter2D (Collider2D other)
    {
        OnTriggerStay2D (other);
    }
    
    void OnTriggerStay2D (Collider2D other)
    {
        if (explosionDone)
            return;
            
        Target target = other.gameObject.GetComponent<Target> ();
        FrozenEffect f = other.gameObject.GetComponent<FrozenEffect> ();
        
        // do not retarget frozen targets
        if (f != null) 
            return;

        if (! (target is Unit))
            return;

        if (target != null && target.Allegiance != mSource.Allegiance) {
            target.gameObject.AddComponent ("FrozenEffect");
            f = other.gameObject.GetComponent<FrozenEffect> ();
            f.target = target.GetComponent<Target> ();
            f.Freeze ();
    
            int damage = (int)WeaponUpgrades.GetStat(WeaponType.IceWand, WeaponStat.Damage);
                                                                                                                                        
            target.Damage (damage);
            if (target.IsDead && mSource.Allegiance == Allegiance.Rodelle)
                UnitUpgrades.AddToExperience (mSource.UnitType, 1);
        }
    
    }
    
    Unit mSource;
    
    public void SetParameters (Unit src, Target target)
    {
        mSource = src;
        transform.position = target.Position;
        StartExplosion ();
    }
    
    private void StartExplosion ()
    {
        mEmitterPrefab = (GameObject)Instantiate (emitter);
        mEmitterPrefab.transform.position = this.transform.position;
        
        int sortingOrder = (int)(4 * (-this.transform.position.y + Camera.main.orthographicSize));
        mEmitterPrefab.GetComponent<Renderer> ().sortingOrder = (int)(sortingOrder);        
        
        kStartTime = Time.time;
    }
    
    
}
