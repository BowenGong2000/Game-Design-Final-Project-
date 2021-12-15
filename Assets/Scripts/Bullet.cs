using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Player temp;
    void Start()
    {
        temp = FindObjectOfType(typeof(Player)) as Player ;
    }
    void Update(){
        //if bullet is too far from player, destroy bullet
        if((Mathf.Abs(transform.position.x - temp.transform.position.x) > 50) || (Mathf.Abs(transform.position.y - temp.transform.position.y) > 50)){
            Destroy(this.gameObject);
        }  
    }
}
