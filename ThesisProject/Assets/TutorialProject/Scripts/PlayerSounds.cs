using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void onMovement()
    {         // Playing step sound when movement input is registered
        if (audioSource.clip != SoundBank.Instance.stepAudio)
        {
            audioSource.clip = SoundBank.Instance.stepAudio;
            audioSource.loop = true;
        }
        // Preventing sound from starting over every time we press multiple buttons
        if (!audioSource.isPlaying) audioSource.Play();
    }
    private void onMovementStop()
    {
        audioSource.Stop();
    }



}
