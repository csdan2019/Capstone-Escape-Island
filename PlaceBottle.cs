using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlaceBottle : MonoBehaviour
{
    public GameObject spawnee;  //object to spawn
    public AudioClip SoundToPlay;
    public float volume;
    AudioSource audio;
  
    //get the audio source at the Start
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    //When the player is in the trigger area
    void OnTriggerStay()
    {
        //get the world manager to check for bools
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();
                   
        //If the Player presses F and has the bottle and hasn't already placed it
        if (Input.GetKeyDown(KeyCode.F) && worldMem.hasBottle && !worldMem.bottlePlaced)
        {
            Instantiate(spawnee, new Vector3(199f, 14.4f, 54.6f), Quaternion.identity);
            GameObject mover = GameObject.Find("Picture");
            mover.transform.position = new Vector3(204.7f, 16f, 53f);
            audio.PlayOneShot(SoundToPlay, volume);
            worldMem.bottlePlaced = true;
        }
    }
}

