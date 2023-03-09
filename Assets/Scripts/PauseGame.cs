using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject hudElements;

    private bool isPaused = false;

    void Update()
    {
        TogglePause();
    }

    void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true) 
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenu.SetActive(false);
        hudElements.SetActive(true);
        isPaused = false;
        Time.timeScale = 1;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        hudElements.SetActive(false);
        isPaused = true;
        Time.timeScale = 0;
    }
}
