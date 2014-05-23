using UnityEngine;
using System.Collections;

public class PauseGameButton : MonoBehaviour
{
    public GameObject PauseMenuObject;
    
    // Update is called once per frame
    void Update ()
    {
        if (Time.timeScale == 1) {// If paused do nothing

            if (Input.GetKeyDown (KeyCode.Space)) {
                OnMouseDown ();
            }
        }
    }

    void OnMouseDown ()
    {
        if (Time.timeScale == 1) {
            PauseMenuObject.SetActive (true);
            Time.timeScale = 0;
        }
    }
}
