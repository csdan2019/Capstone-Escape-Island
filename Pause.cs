using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] GameObject pauseMenuUI;

    //grabing necessary character object
    GameObject FPScontroller;
    GameObject PlayerArms;
    GameObject HudDisplay;
    
    // Set variables to game components
    private void Start()
    {
        FPScontroller = GameObject.FindGameObjectWithTag("Player");
        PlayerArms = FPScontroller.transform.GetChild(2).gameObject;
        HudDisplay = GameObject.FindGameObjectWithTag("HUD");
    }

    /* This function will Pause the game if the Escape key is pressed
     * and the game is not already paused.  Otherwise, it will resume
     * function on Esc being pressed again */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            isPaused = !isPaused;

        if (isPaused)
            PauseGame();

        else
            ResumeGame();
    }

    /* Resume Game reactivates the layers with the HUD and the PlayerArms
     * disables the Pause Menu and restarts time and audio */
    public void ResumeGame()
    {
        PlayerArms.SetActive(true);
        HudDisplay.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        isPaused = false;
        Cursor.visible = false;
    }

    /* Pausing the game disables the cursor lock, deactivates the PlayerArms
     * and HUD and freezes time and audio */
    void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PlayerArms.SetActive(false);
        HudDisplay.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    //Calls the application to exit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
