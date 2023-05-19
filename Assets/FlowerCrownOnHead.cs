using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FlowerCrownOnHead : MonoBehaviour {
    private Interactable interactable;
    private Rigidbody rigidbody;

    //public GameObject flowerCrownPrefab;
    public GameObject VRCamera;
    GameObject duplicatedObject;

    bool attachedToHand = false;
    bool hasBeenGrabbed = false;

    public bool isOnHead = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Wreath of flowers"))
        {
            FlowerCrownOnHead otherFlower = other.gameObject.GetComponent<FlowerCrownOnHead>();
            if (otherFlower.attachedToHand == false && otherFlower.hasBeenGrabbed == true)
            {
                GetComponent<MeshRenderer>().enabled = true;
                Destroy(other.gameObject);

                this.gameObject.AddComponent<Throwable>();

                rigidbody = GetComponent<Rigidbody>();
                interactable = GetComponent<Interactable>();

                // Subscribe to the OnDetachedFromHand event
                interactable.onDetachedFromHand += OnDetachedFromHand;


                // Disable gravity when the player grabs the object
                rigidbody.useGravity = false;

                duplicatedObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform.parent);
                duplicatedObject.SetActive(false);
            }
        }
    }

    private void OnDetachedFromHand(Hand hand)
    {
        attachedToHand = false;
        // Enable gravity when the player lets go of the object
        rigidbody.useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
        duplicatedObject.SetActive(true);
        duplicatedObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(duplicatedObject.GetComponent<Throwable>());
        Destroy(duplicatedObject.GetComponent<Rigidbody>());
        Destroy(duplicatedObject.GetComponent<Interactable>());
        duplicatedObject.GetComponent<BoxCollider>().enabled = false;

        //Instantiate new one
        //GameObject flowerCrown = Instantiate(flowerCrownPrefab, VRCamera.transform, false);
    }

    protected virtual void OnAttachedToHand(Hand hand)
    {
        attachedToHand = true;
        hasBeenGrabbed = true;
    }
}
