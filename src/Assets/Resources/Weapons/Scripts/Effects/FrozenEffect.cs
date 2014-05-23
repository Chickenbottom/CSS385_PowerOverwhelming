using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrozenEffect : MonoBehaviour
{
    public Target target;
    private float mDuration = 3f;
    private float timer;
    
    public void Update ()
    {
        if (Time.time - mStartTimer > mDuration)
            Unfreeze ();
    }
    
    float mStartTimer;

    public void Freeze ()
    {
        if (target is Tower)
            return;
            
        mStartTimer = Time.time;
        
        List<Unit> squad = target.GetComponent<Unit> ().Squad.SquadMembers;
        
        foreach (Unit u in squad) {
            u.BuffMovement(0f, mDuration);
        }
    }
    
    private void Unfreeze ()
    {
        Destroy (this);
    }
}
