using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    public Animator animator;
    // face right or face left
    bool fRight = false;
    public float speed  = 3.0f;
    public SpriteRenderer spRenderer;
    private float slimeLife;
    private float MaxSlimeLife = 2;
    float startX;
    public float distance = 2f;
    //public HealthBar hBar;// slime's health bar
    // Start is called before the first frame update

    // audio source
    public AudioClip bulletSnd;
    AudioSource _audioSource;
    void Start()
    {
        //spRenderer = GetComponent<SpriteRenderer>();
        slimeLife = MaxSlimeLife;
        //hBar.SetLife(slimeLife, MaxSlimeLife);
        startX = transform.position.x;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fRight){
            moveRight();
        }
        else{
            moveLeft();
        }

        if (transform.position.x <= startX-distance){
            fRight = true;
            spRenderer.flipX = false;
        }
        else if (transform.position.x >= startX+distance) {
            fRight = false;
            spRenderer.flipX = true;
        }
    }

    private void flip(){
        transform.Rotate(0f,180f,0f);
    }

    private void takeShot(){
        slimeLife -= 1;
        if (slimeLife <= 0)
        {
            _audioSource.PlayOneShot(bulletSnd);
            Destroy(this.gameObject);
        }
    }
    void moveRight(){
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    void moveLeft(){
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Bullet"){
            takeShot();   
            Destroy(other.gameObject);
        }
           
    }
}