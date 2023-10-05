using UnityEngine;

public class ClickSoundLimitedTime : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;
    private float clickDuration = 5f;
    private bool canClick = false;
    private float clickStartTime;

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if an AudioClip is assigned
        if (clickSound == null)
        {
            Debug.LogError("Click sound is not assigned!");
        }

        StartClicking();

    }

    private void OnMouseDown()
    {
        Debug.Log(canClick);
        if (canClick)
        {
            // Play the click sound
            if (audioSource != null && clickSound != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
            else  {
                Debug.Log("virge");
            }
        }
    }

    public void StartClicking()
    {
        canClick = true;
        clickStartTime = Time.time;
    }

    private void Update()
    {
        if (canClick && Time.time - clickStartTime >= clickDuration)
        {
            canClick = false;
        }
    }
}
