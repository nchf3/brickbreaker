using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaddleScript : MonoBehaviour
{
    // paddle parameters
    public float speed = 5f;
    public float left_limit;
    public float right_limit;

    // used to access ui inputs
    public UiScript ui;

    // Use FixedUpdate to call this method at a fixed frame rate 
    void FixedUpdate(){
        // move the paddle according with the keyboard arrow
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime * ui.get_arrow_direction());

        // android control for the paddle
        if(ui.do_right_move()) transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
        if(ui.do_left_move()) transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);

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
}
