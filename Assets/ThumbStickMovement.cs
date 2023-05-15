using UnityEngine;
using Valve.VR;

public class ThumbStickMovement : MonoBehaviour {
    public SteamVR_Input_Sources handType; // The hand that holds the controller
    public SteamVR_Action_Vector2 thumbstickAction; // The thumbstick action

    public float movementSpeed = 3f; // Speed of movement

    private CharacterController characterController;

    private void Start() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        Vector2 thumbstickValue = thumbstickAction.GetAxis(handType);

        // Calculate the movement direction based on thumbstick input
        Vector3 movementDirection = new Vector3(thumbstickValue.x, 0f, thumbstickValue.y);
        movementDirection = transform.TransformDirection(movementDirection);
        movementDirection *= movementSpeed * Time.deltaTime;

        // Move the character controller
        characterController.Move(movementDirection);
    }
}
