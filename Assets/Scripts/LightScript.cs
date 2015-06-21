
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightScript : MonoBehaviour
{
    TrailRenderer trail;
    float trailCatchUpTime;
    float timer = 1;
    bool hitWall = false;
    void Start () 
    {
        trail = gameObject.GetComponent<TrailRenderer>();
        trailCatchUpTime = trail.time; 

    }

    void Update ()
    {
        
    }

    void FixedUpdate()
    {
        if (hitWall) {
            timer--;
        } 
        

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }


	void OnCollisionEnter2D (Collision2D col)
	{
        if (col.gameObject.layer == 9)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            timer = trailCatchUpTime;
            hitWall = true;
            
           
        }
	}
}