using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : MonoBehaviour
{
    public float jumpForce = 10f;
    public float horizontalForce = 5.0f;
    public GameObject player;
    public Rigidbody2D rb;
    public bool onGround = false;
    public Transform bossFeet;
    public LayerMask groundLayer;
    private void Jump()
    {
        if (onGround){
            var dir = player.transform.position.x < transform.position.x ? -1 : 1;
            rb.AddForce(new Vector2(horizontalForce * dir, jumpForce));
        }
    
    }

    void FixedUpdate(){
        onGround = Physics2D.OverlapCircle(bossFeet.position, .3f, groundLayer);
        Jump();
    }
}
