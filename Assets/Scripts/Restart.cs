using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (CrossPlatformInputManager.GetButtonDown("Pause"))
        {
            LoadGame();
        } 
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("StartMenu");
        PauseMenu.GameIsPaused = false;
    }
}
