using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 0.5f;
    public float distance = 5f;
    public bool moveRight = false;
    float startX;

    void Start() {
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // pausemenu
        if (PauseMenu.GameIsPaused) return;
        Vector2 newPosition = transform.position;
        if(moveRight){
            newPosition.x = Mathf.SmoothStep(startX, startX+distance, Mathf.PingPong(Time.time * speed,1));
        }else{
            newPosition.x = Mathf.SmoothStep(startX, startX-distance, Mathf.PingPong(Time.time * speed,1));
        }
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.transform.SetParent(null);
        }
    }
}