using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{   
    // ball parameters
    public float ball_speed = 5f;

    bool ball_launch = false;
    bool direction = true;
    Vector3 ball_direction = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // launch the ball when statring condition is detected
        if(Input.GetKey("space"))
        {
           ball_launch = true; 
        }

        // move the ball when starting condition is detected
        if(ball_launch)
        {
            // move the ball following the computed vector
            transform.Translate(ball_direction * ball_speed * Time.deltaTime);
        }
    }

    // detect a collision with the ball
    void OnCollisionEnter(Collision collision)
    {
        // compute the new direction vector
        if(direction)
        {
            direction = false;
            ball_direction = Vector3.back;
        } 
        else 
        {
            direction = true;
            ball_direction = Vector3.forward;
        }
    }
}
