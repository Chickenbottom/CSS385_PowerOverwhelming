using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerTargets : MonoBehaviour {

    private List<Bounds> bounds;
    private List<Tower> targets;


	void Start () {
        bounds = new List<Bounds>();
        targets = new List<Tower>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddTower(Tower target)
    {
        CircleCollider2D collider = target.GetComponent<CircleCollider2D>();
        bounds.Add(new Bounds(new Vector3(collider.center.x, collider.center.y, 0f), new Vector3(collider.radius, collider.radius, 0f)));
        targets.Add(target);
    }

    public int ValidMousePos(Vector3 mousePos)
    {
        foreach (Bounds b in bounds)
        {
            if (b.Contains(mousePos))
            {
                return bounds.IndexOf(b);
            }
        }
        return -1;
    }

    public Tower GetTarget(int index)
    {
        return targets[index];
    }

}
