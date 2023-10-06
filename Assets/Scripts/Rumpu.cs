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

    private float elapsedTime;
    private float minTime;
    private float maxTime;
    private float expectedBeatTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (clickSound == null)
        {
            Debug.LogError("Audio clip not assigned to clickSound field!");
        }

        // Load the initial rhythm pattern
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

            // Initialize values for the new pattern
            currentBeatIndex = 0;
            isFirstPress = false;
        }
        else
        {
            Debug.LogError("LevelManager not found in the scene!");
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

        if (rhythmPattern != null)
        {
            if (currentBeatIndex < rhythmPattern.Count)
            {
                elapsedTime = Time.time - startTime; // Calculate time since the first press

                // Check if the elapsed time is within the range of the expected beat time plus/minus beatTolerance
                expectedBeatTime = rhythmPattern[currentBeatIndex];
                minTime = expectedBeatTime - beatTolerance;
                maxTime = expectedBeatTime + beatTolerance;

                if (elapsedTime >= minTime && elapsedTime <= maxTime)
                {
                    Debug.Log("Beat Matched!");
                    audioSource.PlayOneShot(clickSound);
                    currentBeatIndex++;

                    if (currentBeatIndex == rhythmPattern.Count)
                    {
                        // If the current beat is the last one in the pattern, load the next level
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
                    }
                }
                else
                {
                    Debug.Log("Beat Missed!");
                    Debug.Log("Expected Beat Time: " + expectedBeatTime);
                    Debug.Log("Elapsed Time: " + elapsedTime);
                }
            }
        }
    }
}
