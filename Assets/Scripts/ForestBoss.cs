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
    private void Jump()
    {
        if (onGround && !jump){
            var dir = player.transform.position.x < transform.position.x ? -1 : 1;
            rb.AddForce(new Vector2(horizontalForce * dir, jumpForce));
        }
    
    }
    void Update(){
        dist = Mathf.Abs(player.transform.position.x - transform.position.x);
        // if (onGround)
        // {
        //     this.rb.velocity = Vector2.zero;
        // }
        if (dist < 8){
            jump = true;
        }
        else if (dist >= 8){
            jump = false;
        }
    }
    void FixedUpdate(){
        onGround = Physics2D.OverlapCircle(bossFeet.position, .2f, groundLayer);
        if (jump)
        {
            if (onGround)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        
        Jump();
       
    }
}
