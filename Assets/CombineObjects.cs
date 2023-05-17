using UnityEngine;
using System.Collections.Generic;

public class CombineObjects : MonoBehaviour {
    public string targetTag = "TargetObject";
    public int requiredObjectsCount = 3;
    public GameObject replacementObject;
    private List<GameObject> targetObjects;

    private int currentObjectsCount = 0;



    //private void OnCollisionExit(Collider other)
    //{
    //    if (other.CompareTag(targetTag))
    //    {
    //        currentObjectsCount--;
    //    }
    //}

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

        // Enable the replacement object
        if (replacementObject != null)
        {
            replacementObject.SetActive(true);
        }

        // Reset the counter
        currentObjectsCount = 0;
    }
}