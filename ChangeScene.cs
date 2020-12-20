 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/* This overall class function is used to change scenes from the main island to the Cave
 * It updates the WorldManager to indicate that the player has entered the cave at least
 * once and then loads into Cave scene.  It is located on the door to the Cave. */

public class ChangeScene : MonoBehaviour
{
    public string newScene;
    AsyncOperation sceneLoad;
    public WorldManager Overlord;


    public void OnTriggerEnter(Collider Col)
    {
        if (newScene == "Cave")
        {
            Overlord.hasEnteredCave = true;
        }
        StartCoroutine(LoadZone(newScene));
    }

    IEnumerator LoadZone(string newScene)
    {
        sceneLoad = SceneManager.LoadSceneAsync(newScene);

        while (!sceneLoad.isDone)
        {
            yield return null;
        }

    }
}
