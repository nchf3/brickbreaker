using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{   
    // ball parameters
    private bool ball_move = false;
    private bool ball_start = false;
    private Vector3 ball_initial_velocity = Vector3.forward * ball_speed;
    private Vector3 ball_last_velocity;
    private Rigidbody ball_rb; 
    public const float ball_speed = 5f;

    // used to follow the paddle position
    public Transform paddle;

    // list to register destroyed bricks
    private List<GameObject> destroyed_bricks = new List<GameObject>();

    // used to update score and reset game
    public UiScript ui;

    // Start is called before the first frame update
    void Start()
    {
        ball_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // detect starting condition
        if(ball_start)
        {
            // do this just once
            ball_start = false;

            // enable the ball to move
            ball_move = true; 

            // initial velocity
            ball_rb.velocity = ball_initial_velocity;
        }

        // move the ball according to the game state
        if(!ball_move)
        {
            // follow the paddle if the game is not started
            transform.position = paddle.position;
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
            ui.game_reset();
        }
        else
        {
            // compute the bounce vector
            bounce(ref ball_rb, ref collision);

            // delete the game object if we collide with a brick
            if(collision.gameObject.tag == "brick")
            {
                // compute new score
                update_score(collision);

                // save the object
                destroyed_bricks.Add(GameObject.Find(collision.gameObject.name));
                // hide the object
                collision.gameObject.SetActive(false);
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

    // reset ball and bricks position
    public void reset_ball_and_bricks()
    {
        // reset the starting condition
        reset_ball();

        // activate destroyed bricks
        foreach(GameObject brick in destroyed_bricks)
        {
            brick.SetActive(true);
        }

        // clear the destroyed brick list
        destroyed_bricks.Clear();
    }

    // function to launch the ball
    public void launch_ball()
    {
        ball_start = true;
    }

    // function to reset ball position
    private void reset_ball()
    {
        ball_move = false;
    }

    // update the score according to the destroyed brick
    private void update_score(Collision collision)
    {
        // identify brick material
        Renderer renderer = collision.gameObject.GetComponent<Renderer>();

        switch(renderer.material.name)
        {
            case "Brick_1 (Instance)":
                ui.increase_score(1);
                break;
            
            case "Brick_2 (Instance)":
                ui.increase_score(2);
                break;

            case "Brick_3 (Instance)":
                ui.increase_score(3);
                break;

            default:
                break;
        }
    }
}
