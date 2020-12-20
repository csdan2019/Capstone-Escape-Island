using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class OpenChest : MonoBehaviour
{
    public AudioClip SoundToPlay;
    public GameObject spawnee;
    public float volume;
    AudioSource audio;
    GameObject waterfall;
  
    //get the audio source at the Start
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    //When the Player enters the Collider area
    void OnTriggerStay(Collider hit)
    {

        //get the world manager to check for bools
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        //if the trigger object is the Player
        if (hit.tag == "Player")
        {
            //If the Player presses F, has the necessary objects and hasn't already opened the chest
            if (Input.GetKeyDown(KeyCode.F) && worldMem.hasPicture && worldMem.hasSpear && !worldMem.chestOpened)
            {
                //spawn the sprite, move the waterfall and play the audio clue
                Instantiate(spawnee, new Vector3(190.3f, 20.3f, 50.0f), Quaternion.identity);
                audio.PlayOneShot(SoundToPlay, volume);
                GameObject mover = GameObject.Find("Waterfall");
                mover.transform.position = new Vector3(278.8f, 17.2f, 186.3f);
                worldMem.chestOpened = true;

                //remove the barrier at the lake so Player can get Oars
                Destroy(GameObject.Find("Lake Barrier"));
            }
        }
    }
}

