using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BoatEscape : MonoBehaviour
{
    public AudioClip SoundToPlay;   //audio clip
    public float volume;            //volume
    AudioSource audio;              //audio source
    GameObject CreditScene;         //ending credit scene
    
    void Start()
    {
        //Get the audio source
        audio = GetComponent<AudioSource>();
        
        //set the ending credit scene to false
        CreditScene = GameObject.FindWithTag("Credit");
        CreditScene.SetActive(false);
    }

    void OnTriggerStay()
    {
        
        //Get the World Manager object to check for Oars
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        //if the Player has Pressed F, has the Oars and has not already escaped
        if (Input.GetKeyDown(KeyCode.F) && worldMem.hasOars && !worldMem.hasEscaped)
        {
            //Set escape to true, call audio clip and play end credits
            worldMem.hasEscaped = true;
            audio.PlayOneShot(SoundToPlay, volume);
            CreditScene.SetActive(true);

            //end the game after 30 seconds (giving audio time to play)
            Invoke("GameOver", 30);
        }
    }

    //quit the application when invoked
    void GameOver()
    {
        Application.Quit();
    }
}

