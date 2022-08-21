using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.SceneManagement;

public class UiScript : MonoBehaviour
{
    public int score = 0;
    public TMP_Text score_text;

    // score to reach to win the game
    private const int score_max = 12;

    // for android control
    private bool start_pressed_state = false;

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

    private void display_score(int score)
    {
        score_text.text = "Score : " + score; 
    }

    public void increase_score(int add_score)
    {
        score += add_score;
        display_score(score);  
    }

    public void reset_score()
    {
        score = 0;
        display_score(score);
    }

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

    public void start_pressed()
    {
        start_pressed_state = true;
    }
}
