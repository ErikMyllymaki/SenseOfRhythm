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
    public List<float> rhythmPattern = new List<float>();
    private AudioSource audioSource;
    private int currentBeatIndex = 0;
    private float beatTolerance = 0.2f;
    private bool isFirstPress = false;
    private float startTime;

    private float elapsedTime;
    private float minTime;
    private float maxTime;
    private float expectedBeatTime;

    private bool isLevelFailed = false;
    private float retryDelay = 1.5f;

    private void Start()
    {
        objectSpriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (clickSound == null)
        {
            Debug.LogError("Audio clip not assigned to clickSound field!");
        }

        GetAndSetCurrentLevelRhythmPattern();
    }

    private void GetAndSetCurrentLevelRhythmPattern()
    {
        levelManager = FindObjectOfType<LevelManager>();

        if (levelManager != null)
        {
            rhythmPattern = levelManager.GetCurrentLevelRhythmPattern();
            currentBeatIndex = 0;
            isFirstPress = false;
            isLevelFailed = false;
        }
        else
        {
            Debug.LogError("LevelManager not found in the scene!");
        }
    }

    private void Update()
    {
        if (isLevelFailed)
        {
            retryTime -= Time.deltaTime;
            if (retryTime <= 0)
            {
                EndRetryState();
            }
        }
    }

    private void EndRetryState()
    {
        objectSpriteRenderer.color = Color.white;
        isLevelFailed = false;
        GetAndSetCurrentLevelRhythmPattern();
    }

    private void OnMouseDown()
    {
        if (isLevelFailed)
        {
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
                if (elapsedTime < minTime)
                {
                    Debug.Log("too early");
                    Debug.Log("Beat Missed!");
                    Debug.Log("Expected Beat Time: " + expectedBeatTime);
                    Debug.Log("Elapsed Time: " + elapsedTime);
                    StartRetryState();
                }
                if (elapsedTime > maxTime)
                {
                    Debug.Log("Too late");
                    Debug.Log("Expected Beat Time: " + expectedBeatTime);
                    Debug.Log("Elapsed Time: " + elapsedTime);
                    StartRetryState();
                }
            }
        }
    }

    private void StartRetryState()
    {
        isLevelFailed = true;
        retryTime = retryDelay;
        objectSpriteRenderer.color = Color.red;
    }
}
