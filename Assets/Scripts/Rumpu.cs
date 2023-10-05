using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rumpu : MonoBehaviour
{
    public RhythmPlayer rhythmPlayer;
    public LevelManager levelManager;
    public AudioClip clickSound;
    public List<float> rhythmPattern = new List<float>(); // Make the rhythm pattern public
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

        GetAndSetCurrentLevelRhythmPattern();
    }

       // Method to get the current level's rhythm pattern from the LevelManager
    private void GetAndSetCurrentLevelRhythmPattern()
    {
        // Find the LevelManager in the scene
        levelManager = FindObjectOfType<LevelManager>();

        if (levelManager != null)
        {
            // Get the rhythm pattern for the current level
            rhythmPattern = levelManager.GetCurrentLevelRhythmPattern();
            Debug.Log("Rhythm Pattern in Rumpu:");
            foreach (float beatTime in rhythmPattern)
            {
                Debug.Log("Beat Time: " + beatTime);
            }
        }
        else
        {
            Debug.LogError("LevelManager not found in the scene!");
        }
    }

    // public void SetRhythmPattern(List<float> pattern)
    // {
    //     rhythmPattern = pattern;
    //     Debug.Log("Rhythm Pattern in Rumpu:");
    //     foreach (float beatTime in rhythmPattern)
    //     {
    //         Debug.Log("Beat Time: " + beatTime);
    //     }
    // }

    private void OnMouseDown()
    {
        if (!isFirstPress)
        {
            // This is the first press, so start the player's turn
            isFirstPress = true;
            startTime = Time.time; // Record the start time
            Debug.Log("Player's Turn Started!");
        }

        if (rhythmPattern != null)
        {
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
                    if (levelManager != null)
                    {
                        levelManager.LoadNextLevel();
                        GetAndSetCurrentLevelRhythmPattern(); // Get the new rhythm pattern
                    }

                    // Update the rhythm pattern in the RhythmPlayer
                    rhythmPlayer = FindObjectOfType<RhythmPlayer>();
                    if (rhythmPlayer != null)
                    {
                        rhythmPlayer.GetAndSetCurrentLevelRhythmPattern();
                    }
                     // Load the next level
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
