using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiScript : MonoBehaviour
{
    public int score = 0;
    public TMP_Text score_text;

    // Start is called before the first frame update
    void Start()
    {
        display_score(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increase_score()
    {
        score += 1;
        display_score(score);  
    }

    private void display_score(int score)
    {
        score_text.text = "Score : " + score; 
    }
}
