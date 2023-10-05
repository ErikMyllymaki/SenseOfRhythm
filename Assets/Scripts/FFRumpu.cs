using UnityEngine;

public class ClickSoundLimitedTime : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;
    private float clickDuration = 5f;
    private bool canClick = true; // Start with canClick as true
    private float clickStartTime;
    private int clickCount = 0;

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if an AudioClip is assigned
        if (clickSound == null)
        {
            Debug.LogError("Click sound is not assigned!");
        }
    }

    private void OnMouseDown()
    {
        // Check if you can click and the 5-second duration hasn't elapsed
        if (canClick && Time.time - clickStartTime <= clickDuration)
        {
            // Play the click sound
            if (audioSource != null && clickSound != null)
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

    private void Update()
    {
        // Check if the 5-second duration has elapsed
        if (canClick && Time.time - clickStartTime >= clickDuration)
        {
            canClick = false;
            Debug.Log("Clicks within 5 seconds: " + clickCount);
        }
    }
}
