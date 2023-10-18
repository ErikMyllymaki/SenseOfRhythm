using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFrestart : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D buttonCollider;
    public ClickSoundLimitedTime ffrumpu;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Assign the SpriteRenderer component.
        buttonCollider = GetComponent<Collider2D>(); // Assign the Collider2D component.
        ffrumpu = FindObjectOfType<ClickSoundLimitedTime>();
        DisableRestartButton();
    }

    public void EnableRestartButton()
    {
        spriteRenderer.enabled = true;
        buttonCollider.enabled = true;
    }

    public void DisableRestartButton()
    {
        spriteRenderer.enabled = false;
        buttonCollider.enabled = false;
    }

    private void OnMouseDown()
    {
        ffrumpu.RestartGame();
        DisableRestartButton();
    }

    
}
