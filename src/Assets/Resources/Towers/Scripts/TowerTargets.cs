using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerTargets : MonoBehaviour
{
    private struct Target
    {
        public Bounds b;
        public Tower t;
    }

    private List<Target> targetTowers;

    void Start ()
    {
        targetTowers = new List<Target> ();
    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }

    public void AddTower (Tower target)
    {
        BoxCollider2D collider = target.GetComponent<BoxCollider2D> ();
        Transform transform = target.GetComponent<Transform> ();
        Target newTarget = new Target ();

        Vector3 center = Vector3.Scale (transform.localScale, collider.center);
        Vector3 position = center + transform.position;
        Vector3 size = Vector3.Scale (transform.localScale, collider.size);

        //newTarget.b = new Bounds(new Vector3((collider.center.x * transform.localScale) + transform.position.x, collider.center.y + transform.position.y, 0f), 
        //                         new Vector3((collider.size.x * transform.lossyScale.x), (collider.size.y * transform.lossyScale.y), 0f));
        newTarget.b = new Bounds (position, size);
        newTarget.t = target;
        targetTowers.Add (newTarget);
    }

    public int ValidMousePos (Vector3 mousePos)
    {
        foreach (Target targ in targetTowers) {
            if (targ.b.Contains (mousePos)) {
                return targetTowers.IndexOf (targ);
            }
        }
        return -1;
    }

    public Tower GetTarget (int index)
    {
        return targetTowers [index].t;
    }

    public Tower GetTarget (Vector3 mousePos)
    {
        foreach (Target t in targetTowers) {
            if (t.b.Contains (mousePos)) {
                return t.t;
            }
        }
        return null;
    }

}
