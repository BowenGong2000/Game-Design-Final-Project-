using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestBoss : MonoBehaviour
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
    
    //blow
    public float blowSpeed;

    // phase 2
    //public float smoothVal;
    public float speed = 15f;
    private int reverse = -1;

    public bool move  = true;
    // animation
    public Animator anim;

    // health system
    public HealthBar healthBar;
    public float correction;
    
    
    void Start()
    {
        healthBar.SetHealth(100);
        StartCoroutine(idle());
        //StartCoroutine(angry());
    }
    void Update(){
        //dist = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (PauseMenu.GameIsPaused) return;
        dist = (player.transform.position.x - transform.position.x);

        // health bar
        Vector3 monsterPosition = new Vector3(transform.position.x, 
        transform.position.y + correction, transform.position.z); // we need to correct the position of the bar
        healthBar.GetComponent<Transform>().position = Camera.main.WorldToScreenPoint(monsterPosition); 
        healthBar.SetHealth(PublicVars.health);
        }

    IEnumerator idle(){
        yield return new WaitForSeconds(2);
        //Debug.Log(health);
        //takeDamage(1);
        if (PublicVars.health < 50)
        {
            Debug.Log("superAngry");
            StartCoroutine(superAngry());
        }

        else if (PublicVars.health< 80)
        {
            Debug.Log("angry");
            StartCoroutine(angry());
        }
        else
        {
            Debug.Log("blow");
            anim.SetBool("Blow", true);
            PublicVars.blow = true;
            yield return new WaitForSeconds(3);
            anim.SetBool("Blow", false);
            PublicVars.blow = false;
            StartCoroutine(idle());
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Bullet"){
            takeDamage(5);
            Destroy(other.gameObject);
        }
    }

    IEnumerator angry()
    {
        
        anim.SetBool("Angry", true);
        PublicVars.leaveSpawn = true;
        yield return new WaitForSeconds(4);
        PublicVars.leaveSpawn = false;
        StartCoroutine(idle());
    }

    IEnumerator superAngry()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("SuperA", true);
        PublicVars.phase3 = true;

        StartCoroutine(idle());
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
    
}
