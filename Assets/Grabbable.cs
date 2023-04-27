using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Grabbable : MonoBehaviour
{
    private Interactable interactable;
    private Rigidbody rigidbody;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        interactable.onAttachedToHand += OnGrab;
        interactable.onDetachedFromHand += OnRelease;
    }

    private void OnDisable()
    {
        interactable.onAttachedToHand -= OnGrab;
        interactable.onDetachedFromHand -= OnRelease;
    }

    private void OnGrab(Hand hand)
    {
        // Attach the object to the hand
        rigidbody.isKinematic = true;
        transform.SetParent(hand.transform);
        hand.HoverLock(interactable);
        Debug.Log("GRAB HER");
    }

    private void OnRelease(Hand hand)
    {
        // Detach the object from the hand
        rigidbody.isKinematic = false;
        transform.SetParent(null);
        hand.HoverUnlock(interactable);
        // Throw the object in the direction of the hand's velocity
        rigidbody.velocity = hand.GetTrackedObjectVelocity();
        rigidbody.angularVelocity = hand.GetTrackedObjectAngularVelocity();
        Debug.Log("RELEASE HER");
    }
}
