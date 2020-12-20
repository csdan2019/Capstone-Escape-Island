using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCollisionTrigger : MonoBehaviour
{
    //audio variable
    private bool hasPlayed = false;
    public AudioClip SoundToPlay;
    public float volume;
    AudioSource audio;
     
    //Find the audio source on Start
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        //only trigger if audio clue hasn't already played
        if (!hasPlayed)
        {
            //play audio at volume and then set bool to true 
            //so audio doesn't play again
            audio.PlayOneShot(SoundToPlay, volume);
            hasPlayed = !hasPlayed;
        }
    }
}
