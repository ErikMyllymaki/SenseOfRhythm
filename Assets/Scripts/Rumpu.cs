using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rumpu : MonoBehaviour
{
    public RhythmPlayer rhythmPlayer;
    public AudioClip clickSound;
    private AudioSource audioSource;
    private int currentBeatIndex = 0;
    private float beatTolerance = 0.2f;
    private bool isFirstPress = false; 
    private float startTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (clickSound == null)
        {
            Debug.LogError("Audio clip not assigned to clickSound field!");
        }

        if (rhythmPlayer != null)
        {
            List<float> rhythmPattern = rhythmPlayer.GetRhythmPattern();
            Debug.Log("Rhythm Pattern in Rumpu:");
            foreach (float beatTime in rhythmPattern)
            {
                Debug.Log("Beat Time: " + beatTime);
            }
        }
        else
        {
            Debug.LogError("RhythmPlayer reference is null. Please assign it in the Inspector.");
        }
    }

    private void OnMouseDown()
    {
        if (!isFirstPress)
        {
            // This is the first press, so start the player's turn
            isFirstPress = true;
            startTime = Time.time; // Record the start time
            Debug.Log("Player's Turn Started!");
        }

        if (rhythmPlayer != null)
        {
            List<float> rhythmPattern = rhythmPlayer.GetRhythmPattern();

            if (currentBeatIndex < rhythmPattern.Count)
            {
                float elapsedTime = Time.time - startTime; // Calculate time since the first press

                // Check if the elapsed time is within the range of the expected beat time plus/minus beatTolerance
                float expectedBeatTime = rhythmPattern[currentBeatIndex];
                float minTime = expectedBeatTime - beatTolerance;
                float maxTime = expectedBeatTime + beatTolerance;

                if (elapsedTime >= minTime && elapsedTime <= maxTime)
                {
                    Debug.Log("Beat Matched!");
                    audioSource.PlayOneShot(clickSound);
                    currentBeatIndex++;

                    if (currentBeatIndex == rhythmPattern.Count)
                    {
                        Debug.Log("All Beats Matched!");
                    }
                }
                else
                {
                    Debug.Log("Beat Missed!");
                }
            }
        }
    }

}
