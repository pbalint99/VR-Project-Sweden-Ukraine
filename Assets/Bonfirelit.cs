using System.Collections.Generic;
using UnityEngine;

public class Bonfirelit : MonoBehaviour
{
    public GameObject bonFire;
    public AudioClip fireSound;

    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component from the bonFire GameObject
        audioSource = bonFire.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If no AudioSource component is found, add one
            audioSource = bonFire.AddComponent<AudioSource>();
        }
        
        // Set the fire sound clip
        audioSource.clip = fireSound;
        // Set other audio source properties (e.g., volume, looping, etc.) as needed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("torch"))
        {
            bonFire.SetActive(true);

            // Play the fire sound
            if (fireSound != null)
            {
                audioSource.Play();
            }
        }
    }
}
