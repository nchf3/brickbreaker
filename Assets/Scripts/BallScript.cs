using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{   
    // ball parameters
    private bool ball_start = false;
    private Vector3 ball_initial_velocity = Vector3.forward * ball_speed;
    private Vector3 ball_last_velocity;
    private Rigidbody ball_rb; 

    public const float ball_speed = 5f;
    public Transform paddle;

    private List<GameObject> destroyed_bricks = new List<GameObject>();

    private const int score_max = 12;
    public UiScript ui;

    // for android control
    private bool start_pressed_state = false;

    // Start is called before the first frame update
    void Start()
    {
        ball_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // launch the ball when starting condition is detected
        if(!ball_start)
        {
            // detect starting condition
            if(Input.GetKey("space") || start_pressed_state)
            {
                // do this just once
                ball_start = true; 

                // reset pressed button on android
                start_pressed_state = false;

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

            // check if we win the game
            if(ui.check_score())
            {
                Debug.Log("WIN : we destroyed all bricks");

                SceneManager.LoadScene("Scenes/win_menu");
            }
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

    // method to reset game parameters
    private void game_reset()
    {
        // reset the starting condition
        ball_start = false;

        // activate destroyed bricks
        foreach(GameObject brick in destroyed_bricks)
        {
            brick.SetActive(true);
        }

        // clear the destroyed brick list
        destroyed_bricks.Clear();

        // reset the score
        ui.reset_score();
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

    public void start_pressed()
    {
        start_pressed_state = true;
    }
}
