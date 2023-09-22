using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rumpu : MonoBehaviour
{
    public AudioClip clickSound; // Public field to hold the audio clip
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        // Make sure an audio clip is assigned to the public field
        if (clickSound == null)
        {
            Debug.LogError("Audio clip not assigned to clickSound field!");
        }
    }

    // This method is called when the GameObject's collider is clicked.
    private void OnMouseDown()

    {
        // Play the assigned audio clip
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogError("Audio clip or AudioSource component not set up properly.");
        }
    }
}
