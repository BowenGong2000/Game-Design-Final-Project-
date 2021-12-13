using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        StartCoroutine(idle());
        //StartCoroutine(angry());
    }
    void Update(){
        //dist = Mathf.Abs(player.transform.position.x - transform.position.x);
        //PublicVars.hp -= 1;
        dist = (player.transform.position.x - transform.position.x);
    }
    void FixedUpdate(){
       
    }

    IEnumerator idle(){
        yield return new WaitForSeconds(2);
        Debug.Log(PublicVars.hp);
        if (PublicVars.hp < 40)
        {
            Debug.Log("superAngry");
            StartCoroutine(superAngry());
        }

        else if (PublicVars.hp < 70)
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

    
}
