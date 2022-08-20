using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // detect a collision with the ball
    void OnCollisionEnter(Collision collision)
    {
        // hide the brick
        gameObject.active = false;

        // increase the player score
    }
}
