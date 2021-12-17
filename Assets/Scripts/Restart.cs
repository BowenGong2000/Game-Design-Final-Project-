using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ScreenManager;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        ScreenManager.LoadScene("StartMenu");
    }
}
