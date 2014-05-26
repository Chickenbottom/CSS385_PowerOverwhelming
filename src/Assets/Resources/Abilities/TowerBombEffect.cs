using UnityEngine;
using System.Collections;

public class TowerBombEffect : MonoBehaviour
{

    public void ExplodeAt (Vector3 location, int damage)
    {
        mDamage = damage;
        this.transform.position = location;
        
        int sortingOrder = (int)(4 * (-this.transform.position.y + Camera.main.orthographicSize));
        this.GetComponent<Renderer> ().sortingOrder = (int)(sortingOrder);        
        
        kStartTime = Time.time;
    }
    
    // TODO replace with states
    private bool explosionDone = false;
    
    float kLiveTimer = 1.8f;
    float kExplosionDuration = 0.1f;
    float kStartTime;
    GameObject mEmitterPrefab = null;
    
    private int mDamage;
    
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
        
        if (target == null || ! (target is Unit))
            return;
        
        target.Damage(mDamage);
        //target.gameObject.AddComponent ("StunUnit");
        //target.Stun(2f);
                    
        int damage = (int)WeaponUpgrades.GetStat(WeaponType.IceWand, WeaponStat.Damage);        
    }
}
