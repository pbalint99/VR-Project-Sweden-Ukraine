using UnityEngine;
using System.Collections;

public class EnemyInteraction : MonoBehaviour {
    //private Renderer renderer;
    private Color originalColor;
    //private bool isColliding = false;
    private int hitCount = 0;
    private float hitCooldown = 1f;
    private float hitTimer = 0.0f;
    public ParticleSystem particleSystem;
    Animator animator;
    public float movementSpeed = 5.0f;
    private bool isWalking = false;
    bool isHit = false;

    // Start is called before the first frame update
    void Start() {
        // Get the Renderer component and original color from the enemy object
        //renderer = GetComponent<Renderer>();
        //originalColor = renderer.material.color;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        // If a hit has been registered and the cooldown period has passed, reset the color and hit registration
        //if (isColliding && Time.time >= hitTimer + hitCooldown) {
        //    //renderer.material.color = originalColor;
        //    isColliding = false;
        //}
        if(!isWalking && Input.GetKeyDown(KeyCode.KeypadEnter)) {
            isWalking = true;
        }

        if(isWalking) {
            // Set the "isWalking" parameter of the animator to true
            animator.SetBool("isWalking", true);
            Walk();
            if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
                isWalking = false;
                animator.SetBool("isWalking", false);
            }
        }
    }

    // This method is called when a collision occurs with the enemy's collider
    private void OnCollisionEnter(Collision collision) {
        int otherLayer = collision.gameObject.layer;
        string name = collision.gameObject.name;

        // Do something based on the layer of the other game object
        if (otherLayer == LayerMask.NameToLayer("Grab") || name == "HandColliderRight(Clone)" || name == "HandColliderLeft(Clone)") {
            Debug.Log("HIT");
        } else {
            return;
        }

        // Check if the colliding object has a Rigidbody component and if the cooldown period has passed
        Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
        if (otherRb != null && Time.time >= hitTimer + hitCooldown) {
            // If the colliding object has a Rigidbody, increment the hit count and start the cooldown timer
            hitCount++;
            hitTimer = Time.time;
            StartCoroutine(ManageHitEffects());

            // If the hit count is greater than or equal to 10, destroy the enemy
            if (hitCount >= 10) {
                Destroy(gameObject);
            } else {
                // Otherwise, change the color of the enemy to red and set isColliding to true
                //renderer.material.color = Color.red;
                //isColliding = true;

            }
        }
    }

    private IEnumerator ManageHitEffects() {
        particleSystem.Play();
        animator.SetBool("isHit", true);
        isHit = true;
        yield return new WaitForSeconds(1f);
        animator.SetBool("isHit", false);
        particleSystem.Stop();
        particleSystem.Clear();
    }

    private void Walk() {

        // Move the character forward in the direction it is facing
        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        if (isHit) isWalking = false;
    }

}