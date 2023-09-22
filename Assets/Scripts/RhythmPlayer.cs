using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmPlayer : MonoBehaviour
{
    public AudioClip drumSound; 
    private AudioSource audioSource; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown() {
        Debug.Log("painettu");
    }

}
