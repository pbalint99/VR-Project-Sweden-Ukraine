using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FlowerRingCreator : MonoBehaviour
{
    public GameObject flowerPrefab; // The flower prefab to use for creating the ring
    public int numFlowers = 10; // The number of flowers to use in the ring
    public float ringRadius = 1.0f; // The radius of the flower ring
    public float ringHeight = 0.5f; // The height of the flower ring

    private SteamVR_TrackedObject trackedObj; // The tracked object used for grabbing and releasing flowers
    private List<GameObject> flowersInRing; // The list of flowers currently in the ring

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        flowersInRing = new List<GameObject>();
    }

    void Update()
    {
        if (SteamVR_Input.GetStateDown("GrabPinch", SteamVR_Input_Sources.RightHand))
        {
            // Grab a flower from the flower ring and attach it to the controller
            GameObject flower = GrabFlower();
            if (flower != null)
            {
                flower.transform.parent = transform;
                flower.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else if (SteamVR_Input.GetStateUp("GrabPinch", SteamVR_Input_Sources.RightHand))
        {
            // Release the flower from the controller and add it back to the flower ring
            GameObject flower = transform.GetChild(0).gameObject;
            if (flower != null)
            {
                flower.transform.parent = null;
                flower.GetComponent<Rigidbody>().isKinematic = false;
                ReleaseFlower(flower);
            }
        }
    }

    void Start()
    {
        // Create the flower ring on startup
        CreateFlowerRing();
    }

    void CreateFlowerRing()
    {
        // Create a ring of flowers around the controller
        Vector3 controllerPos = transform.position;
        Quaternion controllerRot = transform.rotation;
        float angleBetweenFlowers = 360.0f / numFlowers;
        for (int i = 0; i < numFlowers; i++)
        {
            float angle = i * angleBetweenFlowers;
            Vector3 flowerPos = controllerPos + Quaternion.Euler(0, angle, 0) * (Vector3.forward * ringRadius);
            flowerPos.y = controllerPos.y + ringHeight;
            GameObject flower = Instantiate(flowerPrefab, flowerPos, controllerRot);
            flowersInRing.Add(flower);
        }
    }

    GameObject GrabFlower()
    {
        // Find the closest flower to the controller and remove it from the flower ring
        GameObject closestFlower = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject flower in flowersInRing)
        {
            float distance = Vector3.Distance(transform.position, flower.transform.position);
            if (distance < closestDistance)
            {
                closestFlower = flower;
                closestDistance = distance;
            }
        }
        if (closestFlower != null)
        {
            flowersInRing.Remove(closestFlower);
        }
        return closestFlower;
    }

    void ReleaseFlower(GameObject flower)
    {
        // Add the released flower back to the flower ring
        flowersInRing.Add(flower);
    }
}
