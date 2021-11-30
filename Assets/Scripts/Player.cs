using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public JoystickMovement joystickMovement;
    public float movespeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (joystickMovement.joystickVec.x != 0) // move when detecting joystick movement in x direction
        {
            rb.velocity = new Vector2(joystickMovement.joystickVec.x * movespeed, 0);
        }
        else // remain still
        {
            rb.velocity = Vector2.zero;
        }
    }
}
