using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.SceneManagement;

public class UiScript : MonoBehaviour
{
    // variables to display text on screen
    public int score = 0;
    public TMP_Text score_text;

    // score to win the game
    private const int score_max = 12;

    // for android control
    private bool start_pressed_state = false;
    private bool right_move = false;
    private bool left_move = false;

    // get access to ball and paddle components
    public BallScript ball;

    // Start is called before the first frame update
    void Start()
    {
        display_score(score);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("space") || start_pressed_state)
        {
            // reset pressed button on android
            start_pressed_state = false;

            // launch the ball
            ball.launch_ball();
        }
        
        // check if we win the game
        if(check_score())
        {
            // load the WIN scene
            SceneManager.LoadScene("Scenes/win_menu");
        }
    }

    // method to reset game parameters
    public void game_reset()
    {
        // reset ball and bricks positions
        ball.reset_ball_and_bricks();

        // reset the score
        reset_score();
    }

    // display score on the screen
    private void display_score(int score)
    {
        score_text.text = "Score : " + score; 
    }

    // increase and display the score
    public void increase_score(int add_score)
    {
        score += add_score;
        display_score(score);  
    }

    // reset the score
    public void reset_score()
    {
        score = 0;
        display_score(score);
    }

    // check if the score equals maximum
    public bool check_score()
    {
        if(score >= score_max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // used by android to launch the ball
    public void start_pressed()
    {
        start_pressed_state = true;
    }

    // get direction from arrow keys
    public float get_arrow_direction(){
        return Input.GetAxis("Horizontal");
    }

    // use by android to check if a button is pressed
    public void hold_right()
    {
        right_move = true;
    }

    // use by android to check if a button is pressed
    public void release_right()
    {
        right_move = false;
    }

    // use by android to check if a button is pressed
    public void hold_left()
    {
        left_move = true;
    }

    // use by android to check if a button is pressed
    public void release_left()
    {
        left_move = false;
    }

    // used by paddle component to check if we need to move
    public bool do_right_move()
    {
        return right_move;
    }

    // used by paddle component to check if we need to move
    public bool do_left_move()
    {
        return left_move;
    }
}
