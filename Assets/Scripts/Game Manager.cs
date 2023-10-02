using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameMode
{
    Copycat,
    Polyrhythm,
    FastFingers
}

public class GameManager : MonoBehaviour
{
    // References to UI elements
    public Text scoreText;
    public Text timerText;
    public Button startButton;

    private int score = 0;
    private float timer = 0f;
    private bool isPlaying = false;

    private void Start()
    {
        // Initialize the game based on the selected scene
        InitializeScene();
    }

    public void StartGame()
    {
        isPlaying = true;
        // Handle game-specific initialization here
    }

    public void EndGame()
    {
        isPlaying = false;
        // Handle game-specific end conditions and logic here
    }

    public void Update()
    {
        if (isPlaying)
        {
            // Update game-specific logic here (e.g., score, timer)
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void InitializeScene()
    {
        // Customize the UI and game rules based on the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Handle scene-specific setup here
        switch (currentScene.name)
        {
            case "CopycatScene":
                // Set up Copycat-specific elements
                break;
            case "PolyrhythmScene":
                // Set up Polyrhythm-specific elements
                break;
            case "FastFingersScene":
                // Set up Fast Fingers-specific elements
                break;
        }
    }
}
