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

    private List<GameObject> destroyed_bricks = new List<GameObject>();
    private int destroyed_bricks_count = 0;
    private const int bricks_count = 6;

    public UiScript ui;

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
            game_reset();
        }
        else
        {
            // compute the bounce vector
            bounce(ref ball_rb, ref collision);

            // delete the game object if we collide with a brick
            if(collision.gameObject.tag == "brick")
            {
                // save the object
                destroyed_bricks.Add(GameObject.Find(collision.gameObject.name));
                // hide the object
                collision.gameObject.SetActive(false);

                // increase destroyed brick counter
                destroyed_bricks_count += 1;

                // compute new score
                compute_score();
            }

            // check if we win the game
            if(destroyed_bricks_count == bricks_count)
            {
                Debug.Log("WIN : we destroyed all bricks");
            }
        }
    }

    // method to compute the bounce vector 
    private void bounce(ref Rigidbody ball_rb, ref Collision collision)
    {
        // compute new ball direction
        Vector3 bounce_direction = Vector3.Reflect(ball_last_velocity.normalized, collision.contacts[0].normal);
        // compute new velocity vector
        ball_rb.velocity = bounce_direction * ball_speed;
    }

    // method to reset game parameters
    private void game_reset()
    {
        Debug.Log("Reset the game");

        // reset the starting condition
        ball_launch = false;

        // reset brick count
        destroyed_bricks_count = 0;

        // activate destroyed bricks
        foreach(GameObject brick in destroyed_bricks)
        {
            brick.SetActive(true);
        }

        // clear the destroyed brick list
        destroyed_bricks.Clear();
    }

    private void compute_score()
    {
        ui.increase_score();
    }
}
