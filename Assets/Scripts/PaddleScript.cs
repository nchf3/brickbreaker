using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaddleScript : MonoBehaviour
{
    public float speed = 5f;
    public float left_limit;
    public float right_limit;

    private bool right_move = false;
    private bool left_move = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Use FixedUpdate to call this method at a fixed frame rate 
    void FixedUpdate(){
        // move the paddle according with the keyboard arrow
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime * Input.GetAxis("Horizontal"));

        if(right_move) transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
        if(left_move) transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);

        // manage the paddle to not cross base limits
        if (transform.position.x >= right_limit)
        {
            transform.position = new Vector3(right_limit, transform.position.y, transform.position.z);
        }

        if (transform.position.x <= left_limit)
        {
            transform.position = new Vector3(left_limit, transform.position.y, transform.position.z);
        }

    }

    public void hold_right()
    {
        right_move = true;
    }

    public void release_right()
    {
        right_move = false;
    }

    public void hold_left()
    {
        left_move = true;
    }

    public void release_left()
    {
        left_move = false;
    }
}
