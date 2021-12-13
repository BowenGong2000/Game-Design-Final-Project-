using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leave : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Player;
    float speed = .05f;
    public Rigidbody2D rb;
    
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Vector2 playerPosition = Player.transform.position;
        Vector2 pos = this.transform.position;
        Vector2 fromEnemyToPlayer = playerPosition - pos;
        
        // Normalize it to length 1
        fromEnemyToPlayer.Normalize();
        
        // Set the speed to whatever you want:
        Vector2 velocity = fromEnemyToPlayer * speed * 50;
        
        // Set the rigidbody velocity
        rb.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if((Mathf.Abs(transform.position.x - Player.transform.position.x) > 50) || (Mathf.Abs(transform.position.y - Player.transform.position.y) > 50)){
            Destroy(this.gameObject);
        }  

    }
}
