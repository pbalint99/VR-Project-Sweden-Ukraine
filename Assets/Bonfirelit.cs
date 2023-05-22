using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Bonfirelit : MonoBehaviour
{
    public GameObject bonFire;
    public AudioClip fireSound;
    private AudioSource audioSource;

    public SteamVR_Behaviour_Pose cameraRigPose;
    public float maxVolume = 1.0f;
    public float maxDistance = 10.0f;

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



    private void Update()
    {
        if (cameraRigPose != null)
        {
            // Get the player's position from the SteamVR Camera Rig
            Vector3 playerPosition = cameraRigPose.transform.position;

            // Calculate the distance between the player and the bonfire
            float distance = Vector3.Distance(playerPosition, transform.position);

            // Calculate the volume based on the distance
            float volume = Mathf.Lerp(0, maxVolume, 1 - Mathf.Clamp01(distance / maxDistance));

            // Set the volume of the audio source
            audioSource.volume = volume;
        }
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
