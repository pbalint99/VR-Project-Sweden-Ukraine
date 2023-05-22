using UnityEngine;
using System.Collections.Generic;

public class CombineObjects : MonoBehaviour
{
    public string targetTag = "TargetObject";
    public int requiredObjectsCount = 3;
    public GameObject replacementObject;
    public GameObject confetti; // New confetti object reference
    private List<GameObject> targetObjects;
    public GameObject circleEffect;
    public AudioClip ConfettiSound;

    private int currentObjectsCount = 0;

    private void Start()
    {
        targetObjects = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("Collided with combining stone: " + collision.gameObject.name);
            targetObjects.Add(collision.gameObject);
            currentObjectsCount++;

            if (currentObjectsCount == requiredObjectsCount)
            {
                Combine();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("Left combining stone: " + collision.gameObject.name);
            targetObjects.Remove(collision.gameObject);
            currentObjectsCount--;
        }
    }

    private void Combine()
    {
        // Destroy all objects with the target tag
        foreach (GameObject targetObject in targetObjects)
        {
            Destroy(targetObject);
        }
        targetObjects = new List<GameObject>();

        // Activate confetti object
        if (confetti != null)
        {
            confetti.SetActive(true);
            PlayConfettiSound(); // Play the confetti sound
        }

        // Enable the replacement object after a delay
        if (replacementObject != null)
        {
            Invoke("ActivateReplacementObject", 1f);
        }

        // Reset the counter
        currentObjectsCount = 0;
    }

    private void ActivateReplacementObject()
    {
        // Enable the replacement object
        replacementObject.SetActive(true);
        // Turn off the CircleSpinAreaFire game object
        // GameObject circleSpinAreaFire = GameObject.Find("CircleSpinAreaFire"); // Replace "CircleSpinAreaFire" with the actual name of the game object
        if (circleEffect != null)
        {
            circleEffect.SetActive(false);
        }
    }



    private void PlayConfettiSound()
    {
        if (ConfettiSound != null)
        {
            AudioSource.PlayClipAtPoint(ConfettiSound, confetti.transform.position);
        }
    }


}
