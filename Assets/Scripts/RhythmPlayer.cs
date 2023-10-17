using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmPlayer : MonoBehaviour
{
    public AudioClip drumSound;
    private List<float> rhythmPattern = new List<float>();
    private int currentBeatIndex = 0;
    private bool isPlayingPattern = false;
    private AudioSource audioSource;
    private float patternStartTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetAndSetCurrentLevelRhythmPattern();
    }

    public void EnableRhythmPlayer()
    {
        // Enable the RhythmPlayer by setting its GameObject active
        gameObject.SetActive(true);
        GetAndSetCurrentLevelRhythmPattern();
    }

    public void DisableRhythmPlayer()
    {
        // Disable the RhythmPlayer by setting its GameObject inactive
        gameObject.SetActive(false);
    }

    public void GetAndSetCurrentLevelRhythmPattern()
    {
        // Find the LevelManager in the scene
        LevelManager levelManager = FindObjectOfType<LevelManager>();

        if (levelManager != null)
        {
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

    private void OnMouseDown()
    {
        if (!isPlayingPattern)
        {
            StartRhythmPattern();
        }
        else
        {
            StopRhythmPattern();
            StartRhythmPattern();
        }
    }

    private void StartRhythmPattern()
    {
        currentBeatIndex = 0;
        isPlayingPattern = true;
        patternStartTime = Time.timeSinceLevelLoad;
    }

    private void StopRhythmPattern()
    {
        isPlayingPattern = false;
    }

    private void Update()
    {
        if (isPlayingPattern)
        {
            float beatTime = rhythmPattern[currentBeatIndex];
            float expectedBeatTime = patternStartTime + beatTime;

            if (currentBeatIndex < rhythmPattern.Count && Time.timeSinceLevelLoad >= expectedBeatTime)
            {
                audioSource.PlayOneShot(drumSound);
                currentBeatIndex++;

                if (currentBeatIndex >= rhythmPattern.Count)
                {
                    Debug.Log("Rhythm pattern finished");
                    isPlayingPattern = false;
                }
            }
        }
    }
}
