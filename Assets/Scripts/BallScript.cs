using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{   
    // ball parameters
    private bool ball_launch = false;
    private Vector3 ball_initial_velocity = Vector3.forward * ball_speed;
    private Vector3 ball_last_velocity;
    private Rigidbody ball_rb; 

    public const float ball_speed = 5f;
    public Transform paddle;

    // Start is called before the first frame update
    void Start()
    {
        ball_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // launch the ball when starting condition is detected
        if(!ball_launch)
        {
            // detect starting condition
            if(Input.GetKey("space"))
            {
                ball_launch = true; 
                // ball initial velocity
                ball_rb.velocity = ball_initial_velocity;
            }
            else
            {
                // follow the paddle if the game is not started
                transform.position = paddle.position;
            }
        }
        else
        {
            // get ball velocity
            ball_last_velocity = ball_rb.velocity;
        }
    }

    // detect a collision with the ball
    void OnCollisionEnter(Collision collision)
    {
        // check if the ball collide with the bottom of the game area
        if(collision.gameObject.name == "Edge_bottom")
        {
            // reset the game
            ball_launch = false;
        }
        else
        {
            // compute new ball direction
            Vector3 bounce_direction = Vector3.Reflect(ball_last_velocity.normalized, collision.contacts[0].normal);
            // compute new velocity vector
            ball_rb.velocity = bounce_direction * ball_speed;
        }
    }
}
