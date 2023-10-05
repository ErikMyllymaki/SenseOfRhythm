using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    // Define your rhythm patterns for each level in the script
    private List<List<float>> levelRhythmPatterns = new List<List<float>>
    {
        // first level
        new List<float>
        {
            0.0f, 0.5f, 1.0f, 1.5f, 2.0f
        },
        // second level
        new List<float>
        {
            0.0f, 0.637f, 1.132f, 1.289f, 1.606f, 1.923f
        },
    };

    
    private int currentLevel = 0; // Current level index

    private void Start()
    {
        // Initialize the first level
        LoadLevel(currentLevel);
    }

    public void LoadNextLevel()
    {
        currentLevel++;

        // Check if there are more levels
        if (currentLevel < levelRhythmPatterns.Count)
        {
            LoadLevel(currentLevel);
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }

    private void LoadLevel(int levelIndex)
    {
        // Access the Rumpu script on the drum object
        Rumpu rumpu = GetComponent<Rumpu>();
        RhythmPlayer rhythmPlayer = GetComponent<RhythmPlayer>();

        // if (rumpu != null)
        // {
        //     // Set the rhythm pattern for the current level
        //     List<float> rhythmPattern = levelRhythmPatterns[levelIndex];
        //     rumpu.SetRhythmPattern(rhythmPattern);
        // }
        // else
        // {
        //     Debug.LogError("Rumpu script not found!");
        // }
    }

    // Add a method to get the rhythm pattern for the current level
    public List<float> GetCurrentLevelRhythmPattern()
    {
        if (currentLevel >= 0 && currentLevel < levelRhythmPatterns.Count)
        {
            return levelRhythmPatterns[currentLevel];
        }
        else
        {
            Debug.LogError("Invalid level index: " + currentLevel);
            return new List<float>();
        }
    }
}