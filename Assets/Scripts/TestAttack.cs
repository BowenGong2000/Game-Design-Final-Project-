using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.tag == "ForestBoss")
       {
           Debug.Log("attack!");
           PublicVars.health -= 10;
       }
    }
}
