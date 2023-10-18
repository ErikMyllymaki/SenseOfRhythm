using UnityEngine;
using UnityEngine.UI;

public class ClickSoundLimitedTime : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;
    private bool canClick = false;
    private float clickStartTime;
    private int clickCount;
    private bool gameEnded = false;
    private SpriteRenderer spriteRenderer;

    public FFrestart restartButtonScript; // Reference to the FFrestart script.

    private float clickDuration = 5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Assign the SpriteRenderer component if it's on the same GameObject.

        if (clickSound == null)
        {
            Debug.LogError("Click sound is not assigned!");
        }
    }

    private void OnMouseDown()
    {
        if (!canClick && !gameEnded)
        {
            StartClicking();
        }
        else if (Time.time - clickStartTime <= clickDuration && canClick)
        {
            // Play the click sound
            if (audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
            else
            {
                Debug.Log("AudioSource or clickSound is missing!");
            }

            // Increment the click count
            clickCount++;
        }
    }

    private void StartClicking()
    {
        clickCount = 0;
        canClick = true;
        clickStartTime = Time.time;
    }

    public void RestartGame()
    {
        Debug.Log("Game restarted");
        // Reset the click count and enable clicking
        clickCount = 0;
        canClick = false;
        gameEnded = false;

        // Enable the restart button
    }

    private void Update()
    {
        if (canClick && Time.time - clickStartTime >= clickDuration)
        {
            canClick = false;
            gameEnded = true;
            Debug.Log("Clicks within 5 seconds: " + clickCount);
            restartButtonScript.EnableRestartButton();
            // RestartGame(); // Call the RestartGame method to enable the restart button.
        }
    }
}
