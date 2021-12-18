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
    public float jumpHeight = 5f;
    public float jumpForce = 500;

    //Animations
    public Animator playerAnimator;

    //health system
    public HealthBar hBar;
    public int maxHealth = 100;
    public int curHealth = 100;

    //bullet
    public GameObject bulletPrefab;
    //public GameObject bulletPrefab2;
    int bulletForce = 50;
    public Transform spawnPos;
    public int levelToLoad = 1;
    public Camera cam;

    // audio source
    public AudioClip bulletSnd;
    AudioSource _audioSource;

    // reposition
    public float reX;
    public float reY;
    public float posY;
    private float reverse = 1;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hBar.SetHealth(maxHealth);
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (PauseMenu.GameIsPaused) return;
        // relocate
        if (transform.localPosition.y <= posY){
            RePosition();
            takeDamage(5);
        }
        //Debug.Log(PublicVars.blow);
        grounded = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);
        if (CrossPlatformInputManager.GetButtonDown("Jump") && !PublicVars.Jump && grounded)
        {
            rb.AddForce(Vector2.up*5000f);
            PublicVars.Jump = true;
            rb.gravityScale = 15;
            // enable the jump animation
            playerAnimator.SetBool("IsJumping", true);
            Debug.Log("Jump");
        } 
        else if (CrossPlatformInputManager.GetButtonDown("Jump") && PublicVars.ableToDoubleJump 
                && !PublicVars.doubleJump && !grounded)
        {
            Debug.Log("DoubleJump");
            rb.AddForce(Vector2.up*8000f);
            PublicVars.doubleJump = true;
            rb.gravityScale = 15;
            // enable the jump animation
            playerAnimator.SetBool("IsJumping", true);
        }

        if (grounded)
        {
            PublicVars.Jump = false;
            PublicVars.doubleJump = false;
            rb.gravityScale = 15;
            // disable the jump animation
            playerAnimator.SetBool("IsJumping", false);
        }

        // attack
        float bulletAngle = 1;

        spawnPos.rotation = Quaternion.Euler(0, 0, bulletAngle);
        if (CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            StartCoroutine(attack());
            
        } 

    }

    IEnumerator attack()
    {
        playerAnimator.SetBool("isAttack", true);
        Debug.Log("Attack!!!!!!!!!");
        _audioSource.PlayOneShot(bulletSnd);
        GameObject newbullet = Instantiate(bulletPrefab, spawnPos.position, 
                Quaternion.Euler(0, 0, 1));
        if (!facingLeft){
            reverse = -1;
        }
        else{
            reverse = 1;
        }
        newbullet.GetComponent<Rigidbody2D>().velocity = reverse * spawnPos.right * bulletForce; 
        yield return new WaitForSeconds(0.03f);
        playerAnimator.SetBool("isAttack", false);
    }
    void FixedUpdate()

    {   //rb = GetComponent<Rigidbody2D>();
        // move when detecting joystick movement in x direction
        if (joystickMovement.joystickVec.x != 0) 
        {
            
            // forest boss blow
            if (PublicVars.blow)
            {
                if (joystickMovement.joystickVec.x <= 0)
                {
                    rb.velocity = new Vector2(joystickMovement.joystickVec.x * movespeed * 3, 0);
                    //rb.AddRelativeForce(new Vector2(-3, 0));
                }
                else
                {
                     rb.velocity = new Vector2(joystickMovement.joystickVec.x * movespeed / 3, 0);
                }
            }
            else{
                rb.velocity = new Vector2(joystickMovement.joystickVec.x * movespeed, 0);
            }

            //activate the walking animation 
            playerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(joystickMovement.joystickVec.x));
        }
        else // remain still
        {
            if (PublicVars.blow)
            {
                rb.velocity = new Vector2 (-5, 0);
            }
            else{
                rb.velocity = Vector2.zero;
            // deactivate walking animation
            }
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
        //tag
       if (other.gameObject.tag == "ForestBoss" || other.gameObject.tag == "CaveBoss")
       {
           takeDamage(10);
       }

       if (other.gameObject.tag == "Leave")
       {
           takeDamage(5);
       }

       if (other.gameObject.tag == "DoubleJump")
       {
           PublicVars.ableToDoubleJump = true;
           Destroy(other.gameObject);
       }
       
    }

    void OnCollisonEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "CaveBoss" || other.gameObject.tag == "Leave" || other.gameObject.tag == "ForestBoss")
       {
           takeDamage(5);
       }
    }    

    void takeDamage(int damage)
    {
        curHealth -= damage;
        hBar.SetHealth(curHealth);
        if (curHealth <= 0)
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           curHealth = 100;
        }
    }

    public void RePosition(){
        this.transform.position = new Vector2(reX,reY); 
    }

}
