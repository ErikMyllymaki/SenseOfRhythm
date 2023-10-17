using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public Rumpu rumpu;
    // public RhythmPlayer rhythmPlayer;
    private bool allLevelsCompleted = false;
    // Define your rhythm patterns for each level in the script
    private List<List<float>> levelRhythmPatterns = new List<List<float>>
    {
        // first level
           new List<float>
        {
             0.0f, 0.355628f, 0.705217f, 0.872358f, 1.073313f, 1.398353f, 1.576731f, 1.757802f, 1.951473f, 2.274430f, 2.463506f
        },
         new List<float>
        {
           0.0f, 0.43f, 0.77f, 0.91f, 1.14f, 1.38f, 1.55f, 1.80f
        },
        // second level
        new List<float>
        {
            0.0f, 0.637f, 1.132f, 1.289f, 1.606f, 1.923f
        },
        new List<float>
        {
            0.0f, 1.0f, 2.0f, 3.0f
        },
    };

    
    public int currentLevel = 0; // Current level index

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
        allLevelsCompleted = true; // Mark all levels as completed
        Debug.Log("All levels completed!");

        // Check if the RestartButton is available and enable it
        RestartButton restartButton = FindObjectOfType<RestartButton>();
        if (restartButton != null)
        {
            restartButton.EnableRestartButton();
        }
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
            // Debug.LogError("Invalid level index: " + currentLevel);
            return new List<float>();
        }
    }

    public void RestartGame()
    {
        rumpu = FindObjectOfType<Rumpu>();
        Debug.Log("game restarted");

        if (rumpu != null )
        {
            currentLevel = 0; // Reset the current level to the first level
            rumpu.GetAndSetCurrentLevelRhythmPattern();
            LoadLevel(currentLevel); // Load the first level
        }
        else
        {
            Debug.Log("Rumpu or RhythmPlayer not found!");
        }
    }


}