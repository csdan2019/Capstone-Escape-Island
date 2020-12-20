using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Brazier : MonoBehaviour
{
    public GameObject spawnee;   //object to spawn
    public GameObject destroyee; //object to destroy
    public Brazier dependent;    //dependent brazier for order
    public Transform location;   //place to spawn
    public bool isLit = false;   //is brazier lit?
    public bool isFirst = false; //is brazier first?
    public bool isLast = false;  //is brazier last?
    public AudioClip SoundToPlay; //audio clip
    public int volume;
    AudioSource audio;
   
    //get the audio source at the Start
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    
    //while the player is in the trigger area
    void OnTriggerStay()
    {
        //Get the World Manager to check for bools
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        //If the Player has the Torch from the Beach and has pressed F
        if (Input.GetKeyDown(KeyCode.F) && worldMem.hasTorch)
        {
            //Is this the first brazier and not already lit?
            if (isFirst && !isLit)
            {
                //create a lit brazier and set isLit to true
                Instantiate(spawnee, location);
                isLit = true;
                worldMem.northBrazierLit = true;
            }

            //If not the first brazier and not lit
            else if (dependent.isLit && !isLit)
            {
                //create a lit brazier and set isLit to true
                //update world manager for appropriate object
                Instantiate(spawnee, location);
                isLit = true;
                if (this.gameObject.name == "BrazierPuzzleSouth")
                    worldMem.southBrazierLit = true;
                if (this.gameObject.name == "BrazierPuzzleEast")
                    worldMem.eastBrazierLit = true;

                //If this is the last brazier (e.g. West Brazier)
                if (isLast)
                {
                    //move Sword into place
                    GameObject mover = GameObject.Find("Sword");
                    mover.transform.position = new Vector3(551.5f, 27.2f, 600f);
                    
                    //destroy the scroll, update WorldManager and play audio
                    Destroy(destroyee);
                    worldMem.westBrazierLit = true;
                    audio.PlayOneShot(SoundToPlay, volume);
                }
            }
        }
    }

}
