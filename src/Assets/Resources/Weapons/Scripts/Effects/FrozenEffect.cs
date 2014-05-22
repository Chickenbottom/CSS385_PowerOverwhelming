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
    float originalCharge;
    float originalMovement;

    public void Freeze ()
    {
        if (target is Tower)
            return;
            
        mStartTimer = Time.time;
        
        List<Unit> squad = target.GetComponent<Unit> ().Squad.SquadMembers;
        
        foreach (Unit u in squad) {
            u.BuffMovement(0f, mDuration);
        }
        
        /*
        if (u != null) {
            originalCharge = u.ChargeSpeed;
            originalMovement = u.MoveSpeed;

            

            u.ChargeSpeed = 0f;
            u.MoveSpeed = 0f;
        }   
        */
    }
    
    private void Unfreeze ()
    {
        if (target == null)
            return;
            
        Unit u = target.GetComponent<Unit> ();
        if (u != null) {
            u.ChargeSpeed = originalCharge;
            u.MoveSpeed = originalMovement;
        }
        Destroy (this);
    }
}
