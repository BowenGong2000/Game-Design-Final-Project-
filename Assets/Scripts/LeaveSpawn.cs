using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveSpawn : MonoBehaviour
{
    public GameObject Leave;
    public float startSpawnTime = 1f;
    public float timePerSpawn = 3f;

    public GameObject target;
    void Start()
    {
        InvokeRepeating ("Spawn", startSpawnTime, timePerSpawn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn() {
        if(PublicVars.leaveSpawn && transform.position.y < 0){
            var newLeave = GameObject.Instantiate(Leave, transform.position, Quaternion.identity);
        }
        else if(PublicVars.phase3 && transform.position.y > 0){
            var newLeave = GameObject.Instantiate(Leave, transform.position, Quaternion.identity);
        }
    }
}
