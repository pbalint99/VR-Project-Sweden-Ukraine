using UnityEngine;

public class HullCollisionChecker : MonoBehaviour {
    public GameObject replacementObject; // The object to enable after destroying the "Hull" objects

    private int hullCount = 0; // Counter for the number of "Hull" objects inside the collider

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.EndsWith("Hull")) {
            hullCount++;

            if (hullCount >= 3) {
                DestroyHullObjects();
                EnableReplacementObject();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name.EndsWith("Hull")) {
            hullCount--;
        }
    }

    private void DestroyHullObjects() {
        GameObject[] hullObjects = GameObject.FindGameObjectsWithTag("Hull");

        foreach (GameObject hullObject in hullObjects) {
            Destroy(hullObject);
        }
    }

    private void EnableReplacementObject() {
        replacementObject.SetActive(true);
    }
}
