using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public JoystickMovement joystickMovement;
    public float movespeed;
    private Rigidbody2D rb;
    //jump
    public Transform feet;
    public bool grounded = false;
    public LayerMask groundLayer;
    public Vector2 MoveDir;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Jump
        grounded = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);
        if (CrossPlatformInputManager.GetButtonDown("Jump") && !PublicVars.Jump)
        {
            rb.AddForce(Vector2.up*5000f);
            PublicVars.Jump = true;
            rb.gravityScale = 15f;
        }

        if (grounded)
        {
            PublicVars.Jump = false;
            PublicVars.doubleJump = false;
            rb.gravityScale = 15f;
        }
         if (CrossPlatformInputManager.GetButtonDown("Jump") && PublicVars.Jump && PublicVars.ableToDoubleJump && !PublicVars.doubleJump)
        {
            rb.AddForce(Vector2.up*5000f);
            PublicVars.doubleJump = true;
            rb.gravityScale = 30f;
        }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BossFight")
        {
            SceneManager.LoadScene("BossFight" + PublicVars.levelToLoad);
            PublicVars.levelToLoad ++;
        }
    }

}
