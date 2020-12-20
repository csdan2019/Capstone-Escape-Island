using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/* Basic script that controls the main menu behavior to either load the island
 * for a new game or quit the application if the user selects that option */

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
