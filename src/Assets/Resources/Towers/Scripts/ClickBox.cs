using UnityEngine;
using System.Collections;

public class ClickBox : MonoBehaviour {

    private Bounds clickBox;

	void Start () {
        BoxCollider2D temp = this.gameObject.GetComponent<BoxCollider2D>();
        clickBox = new Bounds(new Vector3(temp.center.x, temp.center.y, 0f), new Vector3(temp.size.x, temp.size.y, 0f));
	}
	
	void Update () {
	
	}

    public Bounds GetClickBoxBounds()
    {
        return clickBox;
    }
}
