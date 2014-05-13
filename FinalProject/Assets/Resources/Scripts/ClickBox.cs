using UnityEngine;
using System.Collections;

public class ClickBox : MonoBehaviour {

    private Bounds mClickBox;

	void Start () {
        BoxCollider2D temp = GameObject.Find("ClickBox").GetComponent<BoxCollider2D>();
        mClickBox = new Bounds(new Vector3(temp.center.x, temp.center.y, 0f), new Vector3(temp.size.x, temp.size.y, 0f));
        Debug.Log(mClickBox);
	}
	
	void Update () {
	
	}

    public Bounds GetClickBocBounds()
    {
        return mClickBox;
    }
}
