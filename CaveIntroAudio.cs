using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveIntroAudio : MonoBehaviour
{
    private bool hasPlayed = false;  //checked if played
    public AudioClip SoundToPlay;
    public float volume;
    AudioSource audio;
     
    //Find the audio source
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        //get worldmanager to check for bools
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        //If player doesn't have spear and hasn't already triggered audio
        //play audio clip introducing them to cave
        if (!hasPlayed && !worldMem.hasSpear)
        {
            audio.PlayOneShot(SoundToPlay, volume);
            hasPlayed = !hasPlayed;
        }
    }
}
