using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBombEffect : MonoBehaviour
{
    public void ExplodeAt (Vector3 location, int damage)
    {
        mDamage = damage;
        this.transform.position = location;
        
        int sortingOrder = (int)(4 * (-this.transform.position.y + Camera.main.orthographicSize));
        this.GetComponent<Renderer> ().sortingOrder = (int)(sortingOrder);        
        
        kStartTime = Time.time;
        mRadius = this.GetComponent<CircleCollider2D>().radius;
    }
    
    // TODO replace with states
    private enum ExplosionState {
        Idle,
        Started,
        Finished,
    }
    private ExplosionState mExplosionState = ExplosionState.Idle;
    
    float kLiveTimer = 1.8f;
    float kExplosionDuration = 0.1f;
    float kStartTime;

    GameObject mEmitterPrefab = null;
    
    private int mDamage;
    private float mRadius;
    
    void Update ()
    {
        if (Time.time - kStartTime > kLiveTimer) {
            Destroy (mEmitterPrefab.gameObject);
            Destroy (this.gameObject);
        }
    }
    
    void OnTriggerEnter2D (Collider2D other)
    {
        OnTriggerStay2D (other);
    }
    
    void OnTriggerStay2D (Collider2D other)
    {
        if (mExplosionState == ExplosionState.Finished)
            return;
            
        mExplosionState = ExplosionState.Started;
        
        Target target = other.gameObject.GetComponent<Target> ();
        
        if (target == null || ! (target is Unit))
            return;
        
        
        this.Knockback (target);
                            
        int damage = (int)WeaponUpgrades.GetStat(WeaponType.IceWand, WeaponStat.Damage);
    }
   
    void FixedUpdate()
    {
        if (mExplosionState == ExplosionState.Started)
            mExplosionState = ExplosionState.Finished;
    }
    
    private void Knockback(Target target)
    {
        if (! (target is Unit))
            return;
        
        List<Unit> squad = target.GetComponent<Unit> ().Squad.SquadMembers;
        
        for (int i = 0; i < squad.Count; ++i) {
            Unit u = squad[i];
            Vector3 toCenter = u.Position - this.transform.position;
            
            float knockbackDistance = toCenter.magnitude;
            float percentFromCenter = (1 - knockbackDistance / mRadius);
            knockbackDistance = percentFromCenter * mRadius;
            knockbackDistance /= 3f;
            
            toCenter.Normalize();
            u.Position += toCenter * knockbackDistance;
            
            // Deals 50-100% damage depending on distance from center
            float percentDamage = Mathf.Max(1f, percentFromCenter + 0.5f); 
            
            target.Damage((int)(mDamage * percentDamage));
            
            u.BuffMovement(0f, 2f); // stun for 2 seconds
        }
    }
}