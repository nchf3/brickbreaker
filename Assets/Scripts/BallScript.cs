using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{   
    // ball parameters
    private bool ball_launch = false;
    private const float ball_speed = 5f;
    private Vector3 ball_initial_velocity = Vector3.forward * ball_speed;
    private Vector3 ball_last_velocity;
    private Rigidbody ball_rb;

    // Start is called before the first frame update
    void Start()
    {
        ball_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // launch the ball when statring condition is detected
        if(Input.GetKey("space") && !ball_launch)
        {
            ball_launch = true; 
        
            // ball initial velocity
            ball_rb.velocity = ball_initial_velocity;
        }

        // get ball velocity
        ball_last_velocity = ball_rb.velocity;
    }

    // detect a collision with the ball
    void OnCollisionEnter(Collision collision)
    {
        // compute new ball direction
        Vector3 bounce_direction = Vector3.Reflect(ball_last_velocity.normalized, collision.contacts[0].normal);
 
        ball_rb.velocity = bounce_direction * ball_speed;
    }
}
