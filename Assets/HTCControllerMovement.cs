using UnityEngine;

public class HTCControllerMovement : MonoBehaviour {
    public float movementSpeed = 3f; // Adjust the speed of movement

    private CharacterController characterController;
    private Transform vrCamera;

    private void Start() {
        characterController = GetComponent<CharacterController>();
        vrCamera = Camera.main.transform;
    }

    private void Update() {
        Vector2 thumbstickInput = new Vector2(Input.GetAxis("HTCThumbstickX"), Input.GetAxis("HTCThumbstickY"));

        // Apply movement based on thumbstick input
        Vector3 movement = new Vector3(thumbstickInput.x, 0f, thumbstickInput.y);
        movement = vrCamera.TransformDirection(movement);
        movement.y = 0f;
        movement.Normalize();
        movement *= movementSpeed;

        // Move the character using CharacterController
        characterController.SimpleMove(movement);
    }
}
