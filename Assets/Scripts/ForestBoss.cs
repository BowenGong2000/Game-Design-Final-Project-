using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : MonoBehaviour
{
    public float jumpForce;
    public float horizontalForce;
    public GameObject player;
    public Rigidbody2D rb;
    public bool onGround = false;
    public Transform bossFeet;
    public LayerMask groundLayer;
    public bool jump = false;
    public float dist;
    private Vector2 velocity;
    //test
    public float y = 60f;
    public float x = 50f;

    // phase 2
    //public float smoothVal;
    public float speed = 15f;
    private int reverse = -1;

    public bool move  = true;
    // animation
    public Animator anim;

    private void Jump()
    {
        if (onGround && !jump){
            var dir = player.transform.position.x < transform.position.x ? -1 : 1;
            rb.AddForce(new Vector2(horizontalForce * dir, jumpForce));
        }
    
    }

    void Start()
    {
        //StartCoroutine(idle());
        StartCoroutine(angry());
    }
    void Update(){
        //dist = Mathf.Abs(player.transform.position.x - transform.position.x);
        //PublicVars.hp -= 1;
        dist = (player.transform.position.x - transform.position.x);
    }
    void FixedUpdate(){
       
    }

    IEnumerator idle(){
        yield return new WaitForSeconds(3);
        anim.SetBool("Blow", true);
        yield return new WaitForSeconds(3);
        anim.SetBool("Blow", false);
        if (PublicVars.hp < 40)
        {
            StartCoroutine(superAngry());
        }

        else if (PublicVars.hp < 70)
        {
            StartCoroutine(angry());
        }

        else
        {
            StartCoroutine(idle());
        }
    }

    IEnumerator angry()
    {
        yield return new WaitForSeconds(2);
        
        anim.SetBool("Angry", true);
        if (move){
            Vector2 tempPos = new Vector2(player.transform.position.x, -2.02f);
            transform.position = Vector2.MoveTowards(transform.position, tempPos, speed * Time.deltaTime);
            yield return new WaitForSeconds(speed * Time.deltaTime);
            move = false;
        }
        else{

        }
        StartCoroutine(angry());
    }

    IEnumerator superAngry()
    {
        
        anim.SetBool("SuperA", true);
        yield return new WaitForSeconds(1);
    }

    
}
