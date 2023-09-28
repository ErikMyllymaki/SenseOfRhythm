using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rumpu : MonoBehaviour
{
    public RhythmPlayer rhythmPlayer;
    public AudioClip clickSound; // Public field to hold the audio clip
    private AudioSource audioSource; // Reference to the AudioSource component
    private float startTime; // Record the start time when you first press the drum
    private bool isRecording = false;

    private void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        rhythmPlayer = GetComponent<RhythmPlayer>();

        // Make sure an audio clip is assigned to the public field
        if (clickSound == null)
        {
            Debug.LogError("Audio clip not assigned to clickSound field!");
        }

            List<float> rhythmPattern = rhythmPlayer.GetRhythmPattern();
            Debug.Log("Rhythm Pattern in Rumpu:");
            foreach (float beatTime in rhythmPattern)
            {
                Debug.Log("Beat Time: " + beatTime);
            } 
    }

    // This method is called when the GameObject's collider is clicked.
    private void OnMouseDown()
    {

        if (!isRecording)
        {
            // Start recording and log the start time
            isRecording = true;
            startTime = Time.time;
        }
        else
        {
           
            // Play the assigned audio clip
            if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
                // Calculate and log the time elapsed since the start time
                float elapsedTime = Time.time - startTime;
                // Debug.Log("Time Elapsed Since Last Click: " + elapsedTime);
            }
            else
            {
                Debug.LogError("Audio clip or AudioSource component not set up properly.");
            }
        }
    }
}
