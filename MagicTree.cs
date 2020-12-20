using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTree : MonoBehaviour
{
    public GameObject spawnee;  //object to spawn
    public AudioClip SoundToPlay;
    public float volume;
    AudioSource audio;

    //get the audio source on Start
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    //a modification on the standard audio collision script
    void OnTriggerStay(Collider hit)
    {
        //get the world manager to check bools
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        //make sure that the hit comes from the Player as there are 
        //multiple overlapping collisions in play on this object
        if (hit.tag == "Player")
        {
            //check that the Player has the sword and the stairs aren't yet spawned
            if (worldMem.hasSword && !worldMem.stairSpawned)
            {
                //spawn stairs, play audio and update world manager
                Instantiate(spawnee, new Vector3(600f, 26f, 468f), Quaternion.identity);
                audio.PlayOneShot(SoundToPlay, volume);
                worldMem.stairSpawned = true;
            }
        }
    }

}
