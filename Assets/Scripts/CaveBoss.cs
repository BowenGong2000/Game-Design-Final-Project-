using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveBoss : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D playerRb;
    public Rigidbody2D rb;
    public bool onGround = false;
    public Transform bossFeet;
    public LayerMask groundLayer;
    public bool jump = false;
    public float dist;
    private Vector2 velocity;

    public float attackRange = 7;
    public bool faceleft = true;

    //blow
    //public float blowSpeed;

    // phase 2
    //public float smoothVal;
    public float speed = 15f;
    private int reverse = -1;

    public bool move = true;
    // animation
    public Animator anim;

    // health system
    public HealthBar healthBar;
    public float correction;


    void Start()
    {
        healthBar.SetHealth(120);  // add 20 hp
        StartCoroutine(idle());
        //StartCoroutine(angry());
    }
    void Update()
    {
        //dist = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (PauseMenu.GameIsPaused) return;
        dist = (player.transform.position.x - transform.position.x);

        // health bar
        Vector3 monsterPosition = new Vector3(transform.position.x,
        transform.position.y + correction, transform.position.z); // we need to correct the position of the bar
        healthBar.GetComponent<Transform>().position = Camera.main.WorldToScreenPoint(monsterPosition);
        healthBar.SetHealth(PublicVars.health);
    }

    IEnumerator idle()
    {
        yield return new WaitForSeconds(2);
        //Debug.Log(health);
        //takeDamage(1);
        if (PublicVars.health < 60)
        {
            Debug.Log("Rage");
            StartCoroutine(Rage());
        }
        else
        {
            // move toward player 

            //Debug.Log("blow");
            if ((player.transform.position.x - transform.position.x)<= attackRange)
            {
                anim.SetBool("Attack", true);
                //PublicVars.blow = true;
                yield return new WaitForSeconds(3);
                anim.SetBool("Attack", false);
                //PublicVars.blow = false;
                StartCoroutine(idle());
            }
           
        }
    }

    IEnumerator RageIdle()
    {
        yield return new WaitForSeconds(2);
        //Debug.Log(health);
        //takeDamage(1);
        if (PublicVars.health < 60)
        {
            Debug.Log("Rage");
            StartCoroutine(Rage());
        }
        else
        {
            // move toward player 

            //Debug.Log("blow");
            // when in range attack
            if ((player.transform.position.x - transform.position.x) <= attackRange + 2)
            {
                anim.SetBool("AttackR", true);
                //PublicVars.blow = true;
                yield return new WaitForSeconds(3);
                anim.SetBool("AttackR", false);
                //PublicVars.blow = false;
                StartCoroutine(RageIdle());
            }

            // flip
            if (player.transform.position.x < transform.position.x)
            {
                this.transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
            }
            // Player is behind the enemy.
            if (player.transform.position.x > transform.position.x)
            {
                this.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            takeDamage(5);
            Destroy(other.gameObject);
        }
    }

    IEnumerator Rage()
    {
        anim.SetBool("GoRage", true);
        // PublicVars.leaveSpawn = true;
        yield return new WaitForSeconds(1);
        // PublicVars.leaveSpawn = false;
        StartCoroutine(RageIdle());
    }

    public void takeDamage(int damage)
    {
        PublicVars.health -= damage;
        healthBar.SetHealth(PublicVars.health);
        if (PublicVars.health <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Cave-1");
        }
    }

    public void flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

}
