using UnityEngine;
using UnityEngine.UI;

public class ClickCounter : MonoBehaviour
{
    public Text clickCountText; // Reference to the UI Text element
    private int clickCount = 0;
    public AudioClip clickSound; // Reference to your click sound

    private void Start()
    {
        // Initialize the UI Text with the click count
        UpdateClickCountText();
    }

    private void UpdateClickCountText()
    {
        // Update the UI Text with the current click count
        clickCountText.text = "Clicks: " + clickCount.ToString();
    }

    private void OnMouseDown()
    {
        // Increment the click count and update the UI Text
        clickCount++;
        UpdateClickCountText();

        // Play the click sound
        AudioSource.PlayClipAtPoint(clickSound, transform.position);
    }

    public void ResetClickCount()
    {
        // Reset the click count to zero and update the UI Text
        clickCount = 0;
        UpdateClickCountText();
    }
}
