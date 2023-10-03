using UnityEngine;
using UnityEngine.UI;

public class FFrumpu : MonoBehaviour
{
    public Text clickCountText;
    public Button resetButton;
    public float gameDuration = 5.0f;
    public AudioClip clickSound; // Reference to your click sound

    public AudioSource audioSource;


    private int clickCount = 0;
    private float timer;
    private bool isGameRunning = false;

    void Start()
    {
        clickCountText.text = "Clicks: " + clickCount;
        timer = gameDuration;

        // Add an onClick event to the reset button
        resetButton.onClick.AddListener(ResetClickCount);
    }

    void Update()
    {
        if (isGameRunning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        clickCount = 0;
        clickCountText.text = "Clicks: " + clickCount;
        timer = gameDuration;
        isGameRunning = true;
    }

    void EndGame()
    {
        isGameRunning = false;
        clickCountText.text = "Game Over! Clicks: " + clickCount;
    }

    void OnMouseDown()
    {
        if (isGameRunning)
        {
            clickCount++;
            clickCountText.text = "Clicks: " + clickCount; // Update the UI Text

            // Play the click sound
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void ResetClickCount()
    {
        clickCount = 0;
        clickCountText.text = "Clicks: " + clickCount; // Update the UI Text
    }
}
