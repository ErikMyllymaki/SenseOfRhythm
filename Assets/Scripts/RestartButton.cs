using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public Rumpu rumpu;
    public LevelManager levelManager;
    private SpriteRenderer spriteRenderer;
    private Collider2D buttonCollider;
    public RhythmPlayer rhythmPlayer; // Reference to the RhythmPlayer script

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager == null)
        {
            Debug.LogError("LevelManager not found in the scene!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        buttonCollider = GetComponent<Collider2D>();

        DisableRestartButton();
    }

    public void EnableRestartButton()
    {
        // Enable the restart button
        spriteRenderer.enabled = true;
        buttonCollider.enabled = true;
        // Disable the RhythmPlayer
        rhythmPlayer.DisableRhythmPlayer();
    }

    public void DisableRestartButton()
    {
        // Disable the restart button
        spriteRenderer.enabled = false;
        buttonCollider.enabled = false;
        // Enable the RhythmPlayer
        rhythmPlayer.EnableRhythmPlayer();
    }

   private void OnMouseDown()
    {
        RestartGame();
        DisableRestartButton();
    }

    public void RestartGame()
    {
        rumpu = FindObjectOfType<Rumpu>();

        if (levelManager != null && rumpu != null)
        {
            Debug.Log("Restart button clicked");
            levelManager.RestartGame();
        }
        else
        {
            Debug.Log("LevelManager, Rumpu, or RhythmPlayer not found!");
        }
    }

}
