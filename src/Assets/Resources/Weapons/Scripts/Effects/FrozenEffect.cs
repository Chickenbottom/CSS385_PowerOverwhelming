using UnityEngine;
using System.Collections;

public class FrozenEffect : MonoBehaviour
{
    public Target target;
    private float mDuration = 5f;
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
        mStartTimer = Time.time;

        Unit u = target.GetComponent<Unit> ();
        if (u != null) {
            originalCharge = u.ChargeSpeed;
            originalMovement = u.MoveSpeed;

            u.ChargeSpeed = 0f;
            u.MoveSpeed = 0f;
        }   
    }
    
    private void Unfreeze ()
    {
        Unit u = target.GetComponent<Unit> ();
        if (u != null) {
            u.ChargeSpeed = originalCharge;
            u.MoveSpeed = originalMovement;
        }
        Destroy (this);
    }
}
