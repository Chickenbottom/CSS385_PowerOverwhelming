using UnityEngine;
using System.Collections;

public class KingRodelle : MonoBehaviour
{

    private float health;


    // Use this for initialization
    void Start()
    {
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            //gameManager game over
        }
    }
    public void hitRodelle(float d)
    {
        health -= d;
    }
    public void healRodelle(float h)
    {
        health += h;
    }

}
