using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmPlayer : MonoBehaviour
{
    public AudioClip drumSound;
    private List<float> rhythmPattern = new List<float>(); // Store the rhythm pattern
    private int currentBeatIndex = 0; // Index to track the current beat
    private bool isPlayingPattern = false; // Flag to indicate whether the rhythm pattern is playing
    private AudioSource audioSource;
    private float patternStartTime; // Store the start time of the pattern

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetRhythmPattern(List<float> pattern)
    {
        rhythmPattern = pattern;
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked");

        if (!isPlayingPattern)
        {
            StartRhythmPattern();
        }
        else
        {
            // Stop the current pattern and start a new one
            StopRhythmPattern();
            StartRhythmPattern();
        }
    }

    private void StartRhythmPattern()
    {
        Debug.Log("PlayRhythmPattern started");
        currentBeatIndex = 0;
        isPlayingPattern = true;
        patternStartTime = Time.timeSinceLevelLoad; // Store the start time
    }

    private void StopRhythmPattern()
    {
        Debug.Log("Stopping RhythmPattern");
        isPlayingPattern = false;
    }

    private void Update()
    {
        if (isPlayingPattern)
        {
            float beatTime = rhythmPattern[currentBeatIndex];
            float expectedBeatTime = patternStartTime + beatTime;

            // Check if it's time to play the next beat
            if (currentBeatIndex < rhythmPattern.Count && Time.timeSinceLevelLoad >= expectedBeatTime)
            {
                audioSource.PlayOneShot(drumSound);
                // Debug.Log("Next beat coming in: " + (expectedBeatTime - Time.timeSinceLevelLoad));
                currentBeatIndex++;

                // Check if the rhythm pattern is finished
                if (currentBeatIndex >= rhythmPattern.Count)
                {
                    Debug.Log("Rhythm pattern finished");
                    isPlayingPattern = false;
                }
            }
        }
    }

    public List<float> GetRhythmPattern()
    {
        return rhythmPattern;
    }

}
