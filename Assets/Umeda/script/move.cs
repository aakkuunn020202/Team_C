using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCapsule : MonoBehaviour
{
    Rigidbody rb;
    float speed = 3.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody> ();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)){
            rb.linearVelocity = transform.forward*speed;
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            rb.linearVelocity = -transform.forward*speed;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            rb.linearVelocity = transform.right*speed;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            rb.linearVelocity = -transform.right*speed;
        }
    }
}
