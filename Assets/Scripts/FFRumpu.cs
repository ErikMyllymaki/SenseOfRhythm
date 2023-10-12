using UnityEngine;
using UnityEngine.UI;

public class ClickSoundLimitedTime : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;
    private bool canClick = false;
    private float clickStartTime;
<<<<<<< Updated upstream
    private int clickCount;
    private bool gameEnded = false;

    // public Button restartButton;
=======
    private float clickDuration = 5f;
>>>>>>> Stashed changes

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if an AudioClip is assigned
        if (clickSound == null)
        {
            Debug.LogError("Click sound is not assigned!");
        }

        // Attach a method to the restart button's click event
        // if (restartButton != null)
        // {
        //     restartButton.onClick.AddListener(RestartGame);
        // }
    }

    private void OnMouseDown()
    {
<<<<<<< Updated upstream
        // Check if you can click and the game hasn't ended
        if (!canClick && !gameEnded)
        {
            StartClicking();
        }
        else if (Time.time - clickStartTime <= clickDuration)
=======
        if (canClick)
>>>>>>> Stashed changes
        {
            // Play the click sound
            if (audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
<<<<<<< Updated upstream
            else
            {
                Debug.Log("AudioSource or clickSound is missing!");
            }

            // Increment the click count
            clickCount++;
=======
>>>>>>> Stashed changes
        }
    }

    private void StartClicking()
    {
        clickCount = 0;
        canClick = true;
        clickStartTime = Time.time;
    }

    private void RestartGame()
    {
        // Reset the click count and enable clicking
        clickCount = 0;
        canClick = false;
        gameEnded = false;
    }

    private void Update()
    {
        // Check if the 5-second duration has elapsed
        if (canClick && Time.time - clickStartTime >= clickDuration)
        {
            canClick = false;
            gameEnded = true;
            Debug.Log("Clicks within 5 seconds: " + clickCount);
        }
    }
}
