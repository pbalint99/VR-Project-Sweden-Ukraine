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

    private void Start()
    {
        if(GetComponent<Interactable>() != null)
        {
            interactable = GetComponent<Interactable>();
            interactable.onDetachedFromHand += OnDetachedFromHand;
            interactable.onAttachedToHand += OnAttachedToHand;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Wreath"))
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
                interactable.onAttachedToHand += OnAttachedToHand;
                
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
                rigidbody.transform.parent = VRCamera.transform;

                duplicatedObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform.parent);
                duplicatedObject.SetActive(false);
                duplicatedObject.GetComponent<FlowerCrownOnHead>().isOnHead = true;
                duplicatedObject.transform.SetParent(VRCamera.transform);
                duplicatedObject.transform.position = VRCamera.transform.position + new Vector3(0.00999999978f, 0.12f, -0.0399999991f);
                duplicatedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void OnDetachedFromHand(Hand hand)
    {
        attachedToHand = false;
        duplicatedObject.transform.SetParent(null);
        duplicatedObject.GetComponent<Rigidbody>().isKinematic = false;
        // Enable gravity when the player lets go of the object
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        GetComponent<BoxCollider>().enabled = true;
        duplicatedObject.SetActive(true);
        duplicatedObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(duplicatedObject.GetComponent<Throwable>());
        Destroy(duplicatedObject.GetComponent<Rigidbody>());
        Destroy(duplicatedObject.GetComponent<Interactable>());
        duplicatedObject.GetComponent<BoxCollider>().enabled = false;
        duplicatedObject.GetComponent<FlowerCrownOnHead>().isOnHead = false;

        //Instantiate new one
        //GameObject flowerCrown = Instantiate(flowerCrownPrefab, VRCamera.transform, false);
    }

    protected virtual void OnAttachedToHand(Hand hand)
    {
        attachedToHand = true;
        hasBeenGrabbed = true;
    }

    //private void FixedUpdate()
    //{
    //    if(isOnHead && !attachedToHand)
    //    {
    //        transform.position = VRCamera.transform.position + new Vector3(0.00999999978f, crownHeight, -0.0399999991f);
    //    }
    //}
}
