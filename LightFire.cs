using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class LightFire : MonoBehaviour
{
    public GameObject spawnee;   //object to spawn
    public AudioClip SoundToPlay;
    public float volume;
    AudioSource audio;
  
    //Get audio source on Start
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    //while the Player is in the trigger area
    void OnTriggerStay()
    {
        //get the world manager to check for bools
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();
                   
        //conditionals to trigger lighting fire event
        if (Input.GetKeyDown(KeyCode.F) && worldMem.hasWood && worldMem.hasLighter && !worldMem.beachFireLit)
        {
            //once triggered spawn the fire
            Instantiate(spawnee, new Vector3(860f, 20.2f, 263f), Quaternion.identity);

            //move the torch into position, play audio and update world manager
            GameObject mover = GameObject.Find("Torch");
            mover.transform.position = new Vector3(862f, 20f, 263.5f);
            audio.PlayOneShot(SoundToPlay, volume);
            worldMem.beachFireLit = true;
        }
    }
}

