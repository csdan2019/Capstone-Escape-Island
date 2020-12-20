using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClick : MonoBehaviour
{
    //audio variables
    public AudioClip SoundToPlay;
    public float volume;
    AudioSource audio;

    //Find the audio source on Start
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    //If the player presses F while in the trigger area, play sound at volume
    //This will allow the player to listen to important clues more than once
    void OnTriggerStay()
    {
        if(Input.GetKeyDown(KeyCode.F))
            audio.PlayOneShot(SoundToPlay, volume);
    }
}
