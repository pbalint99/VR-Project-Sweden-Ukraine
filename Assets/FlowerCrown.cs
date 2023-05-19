using UnityEngine;
using Valve.VR.InteractionSystem;

public class FlowerCrown : MonoBehaviour {
    private bool isGrabbed = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Hand currentHand;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();

        if (!isGrabbed && grabType != GrabTypes.None)
        {
            isGrabbed = true;
            currentHand = hand;
            //hand.AttachObject(gameObject, grabType);
        }
        else if (isGrabbed && grabType == GrabTypes.None)
        {
            isGrabbed = false;
            //currentHand.DetachObject(gameObject);
            currentHand = null;

            // Calculate distance to the camera
            float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

            // Check if the crown is close to the camera
            if (distanceToCamera < 0.5f)
            {
                // Calculate the head position and rotation
                Vector3 headPosition = Camera.main.transform.position;
                Quaternion headRotation = Camera.main.transform.rotation;

                // Set the crown's position and rotation to match the head
                transform.position = headPosition;
                transform.rotation = headRotation;
            }
            else
            {
                // Reset the crown's position and rotation
                transform.position = initialPosition;
                transform.rotation = initialRotation;
            }
        }
    }
}
