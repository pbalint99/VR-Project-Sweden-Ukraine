using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRControllerMovement : MonoBehaviour {
    //public SteamVR_Input_Sources inputSource;  // The input source for this controller
    public SteamVR_Action_Vector2 thumbstickAction;  // The thumbstick action for this controller
    //public float movementSpeed = 3f;  // The speed at which the player moves
    public Transform cameraTransform;
    public AudioSource walkingsound;
    private CapsuleCollider capsuleCollider;

    private void Start() {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update() {
        // Vector2 thumbstickValue = thumbstickAction.GetAxis(inputSource);  // Get the thumbstick value

        
    }

    private void FixedUpdate() {
        // Calculate the movement vector in local space
        Vector3 movementDir = Player.instance.hmdTransform.TransformDirection(new Vector3(thumbstickAction.axis.x, 0, thumbstickAction.axis.y));
        // Move the player in the direction of the thumbstick
        transform.position += Vector3.ProjectOnPlane(Time.deltaTime * movementDir * 5.0f, Vector3.up);

        float distanceFromFloor = Vector3.Dot(cameraTransform.localPosition, Vector3.up);
        capsuleCollider.height = Mathf.Max(capsuleCollider.radius, distanceFromFloor);

        capsuleCollider.center = cameraTransform.localPosition - 0.5f * distanceFromFloor * Vector3.up;

        //play sound if moving

        if (movementDir.magnitude > 0.2f && !walkingsound.isPlaying)
        {
            walkingsound.Play();
        }
        else if(movementDir.magnitude <= 0.2f && walkingsound.isPlaying) {
            walkingsound.Stop();
        }
    }
}
