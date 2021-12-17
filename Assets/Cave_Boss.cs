using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cave_Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    // health system
    public HealthBar healthBar;
    public float correction;
    void Start()
    {
        healthBar.SetHealth(100);

    }

    void Update(){
        // health bar
        Vector3 monsterPosition = new Vector3(transform.position.x - 3, 
        transform.position.y + correction, transform.position.z); // we need to correct the position of the bar
        healthBar.GetComponent<Transform>().position = Camera.main.WorldToScreenPoint(monsterPosition); 
        healthBar.SetHealth(PublicVars.hp);
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Bullet"){
            takeDamage(5);
            Destroy(other.gameObject);
        }
    }

    private void takeDamage(int damage)
    {
        PublicVars.hp -= damage;
        healthBar.SetHealth(PublicVars.hp);
        if (PublicVars.hp <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("End");
        }
    }
}
