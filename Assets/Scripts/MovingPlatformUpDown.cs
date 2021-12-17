using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformUpDown : MonoBehaviour
{
    public float speed = 0.5f;
    public float distance = 5f;
    public bool moveUp = false;
    float startY;

    void Start() {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // pausemenu
        if (PauseMenu.GameIsPaused) return;
        Vector2 newPosition = transform.position;
        if(moveUp){
            newPosition.y = Mathf.SmoothStep(startY, startY+distance, Mathf.PingPong(Time.time * speed,1));
        }else{
            newPosition.y = Mathf.SmoothStep(startY, startY-distance, Mathf.PingPong(Time.time * speed,1));
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
