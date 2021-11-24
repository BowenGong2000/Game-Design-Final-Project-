using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveControl : MonoBehaviour
{
    public float movespeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        TouchMove();
    }

    void TouchMove()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (mousePos.x > 1) // right side is touched
            {
                // move to right
                transform.Translate(movespeed, 0, 0);
            }

            if (mousePos.x < -1) // left side is touched
            {
                // move to left
                transform.Translate(-movespeed, 0, 0);
            }
        }
        
        
        
    }

}
