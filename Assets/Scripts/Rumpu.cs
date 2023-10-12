using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rumpu : MonoBehaviour
{
    private SpriteRenderer objectSpriteRenderer;
    private float retryTime;
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

    private bool isLevelFailed = false; // Track whether the current level is failed
    private float retryDelay = 1.5f; // Delay before retrying the level

    private void Start()
    {
        objectSpriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (clickSound == null)
        {
            Debug.LogError("Audio clip not assigned to clickSound field!");
        }

        // Load the initial rhythm pattern
        GetAndSetCurrentLevelRhythmPattern();
    }

    private void GetAndSetCurrentLevelRhythmPattern()
    {
        levelManager = FindObjectOfType<LevelManager>();

        if (levelManager != null)
        {
            rhythmPattern = levelManager.GetCurrentLevelRhythmPattern();
            // Debug.Log("Rhythm Pattern in Rumpu:");
            // foreach (float beatTime in rhythmPattern)
            // {
            //     Debug.Log("Beat Time: " + beatTime);
            // }

            currentBeatIndex = 0;
            isFirstPress = false;
            isLevelFailed = false;
        }
        else
        {
            Debug.LogError("LevelManager not found in the scene!");
        }
    }

    private void OnMouseDown()
    {
        if (isLevelFailed)
        {
            // The level was failed, but the player is trying again.
            StartCoroutine(RetryLevel());
            return;
        }

        if (!isFirstPress)
        {
            isFirstPress = true;
            startTime = Time.time;
            Debug.Log("Player's Turn Started!");
        }

        if (rhythmPattern != null)
        {
            if (currentBeatIndex < rhythmPattern.Count)
            {
                elapsedTime = Time.time - startTime;

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
                        if (levelManager != null)
                        {
                            levelManager.LoadNextLevel();
                            GetAndSetCurrentLevelRhythmPattern();
                        }

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
                    isLevelFailed = true; // The level is failed
                }
            }
        }
    }

    private IEnumerator RetryLevel()
    {
        retryTime = Time.time + retryDelay;

        // Change the object's color to red
        objectSpriteRenderer.color = Color.red;

        while (Time.time < retryTime)
        {
            yield return null;
        }

        // Reset the object's color to its original state
        objectSpriteRenderer.color = Color.white;

        Debug.Log("You can try again now!");

        // Retry the level
        GetAndSetCurrentLevelRhythmPattern();
    }







}
