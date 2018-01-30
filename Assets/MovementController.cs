using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //speed stuff
    public int cruiseSpeed;
    float deltaSpeed;//(speed - cruisespeed) 

    public int minSpeed;
    public int maxSpeed;
    float accel, decel;

    public Vector3 cameraOffset; //I use (0,1,-3)

    public double speed; // = 7.5;
    private Rigidbody rb;

    float dive;
    double pi = 3.1415;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        speed = cruiseSpeed;
        speed = 10;
        Vector3 movement = new Vector3(0, 0, 5.0f);
        rb.AddForce(movement * (float)speed);
    }


    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.I))
        {
            speed += 5;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }

        }

        if (Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.O))
        {
            speed -= 5;
            if (speed < minSpeed)
            {
                speed = minSpeed;
            }

        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            dive += 1.0f;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            dive -= 1.0f;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if ((dive < 0) && rb.velocity.z < 2.0F)
        {
            dive = 0;
        }


        Vector3 movement = new Vector3(moveHorizontal, moveVertical, dive);                                                                        //Vector3 movement = new Vector3(4.0f, 4.0f, 4.0f)
        rb.AddForce((movement * (float)speed));
        dive = 0;
    }
}