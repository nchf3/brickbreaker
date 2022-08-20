using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiScript : MonoBehaviour
{
    public int score = 0;
    public TMP_Text score_text;

    private const int score_max = 12;

    // Start is called before the first frame update
    void Start()
    {
        display_score(score);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
