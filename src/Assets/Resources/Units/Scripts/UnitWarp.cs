using UnityEngine;
using System.Collections;

public class UnitWarp : MonoBehaviour {
    
    public void DestroyThis()
    {
        Destroy (this.gameObject);
        Destroy (this);
    }
    
	// Use this for initialization
	void Start () {
        Invoke("DestroyThis", 1f);
	}
}
