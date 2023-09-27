using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmPlayer : MonoBehaviour
{
    public AudioClip drumSound; 
    private AudioSource audioSource; 
    private bool isPlayingAudio = false; // Flag to track whether the audio is playing

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (!isPlayingAudio) // Check if the audio is not already playing
        {
            Debug.Log("Clicked");
            if (drumSound != null && audioSource != null)
            {
                StartCoroutine(PlayAudio());
            }
            else
            {
                Debug.LogError("Audio clip or AudioSource component not set up properly.");
            }
        }
    }

    private IEnumerator PlayAudio()
    {
        isPlayingAudio = true; // Set the flag to indicate that the audio is playing

        audioSource.PlayOneShot(drumSound);

        // Wait for the duration of the audio clip
        yield return new WaitForSeconds(drumSound.length);

        isPlayingAudio = false; // Reset the flag when the audio finishes
    }
}
