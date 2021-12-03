using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public JoystickMovement joystickMovement;
    public float movespeed;
    public bool facingLeft = true;
    private Rigidbody2D rb;

    //jump
    public Transform feet;
    public bool grounded = false;
    public LayerMask groundLayer;
    public Vector2 MoveDir;

    //Animations
    public Animator playerAnimator;

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
            // enable the jump animation
            playerAnimator.SetBool("IsJumping", true);
        }

        if (grounded)
        {
            PublicVars.Jump = false;
            PublicVars.doubleJump = false;
            rb.gravityScale = 15f;
            // disable the jump animation
            playerAnimator.SetBool("IsJumping", false);
        }
         if (CrossPlatformInputManager.GetButtonDown("Jump") && PublicVars.Jump && PublicVars.ableToDoubleJump && !PublicVars.doubleJump)
        {
            rb.AddForce(Vector2.up*5000f);
            PublicVars.doubleJump = true;
            rb.gravityScale = 30f;
            // enable the jump animation
            playerAnimator.SetBool("IsJumping", true);
        }
    }
    void FixedUpdate()

    {   // move when detecting joystick movement in x direction
        if (joystickMovement.joystickVec.x != 0) 
        {
            rb.velocity = new Vector2(joystickMovement.joystickVec.x * movespeed, 0);
            //activate the walking animation 
            playerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(joystickMovement.joystickVec.x));
        }
        else // remain still
        {
            rb.velocity = Vector2.zero;
            // deactivate walking animation
            playerAnimator.SetFloat("PlayerSpeed", 0);
        }

        // check facing direction
        if (joystickMovement.joystickVec.x>0&& !facingLeft)
        {
            FlipDirection();
        }
        else if (joystickMovement.joystickVec.x<0 && facingLeft)
        {
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BossFight")
        {
            SceneManager.LoadScene("BossFight" + PublicVars.levelToLoad);
            PublicVars.levelToLoad ++;
        }
        if (other.gameObject.tag == "LevelGate")
        {
            SceneManager.LoadScene("Level" + PublicVars.levelToLoad);
            PublicVars.levelToLoad ++;
        }
    }

}
