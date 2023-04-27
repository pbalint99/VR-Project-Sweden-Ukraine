using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    private Renderer renderer;
    private Color originalColor;
    private bool isColliding = false;
    private int hitCount = 0;
    private float hitCooldown = 0.1f;
    private float hitTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Renderer component and original color from the enemy object
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        // If a hit has been registered and the cooldown period has passed, reset the color and hit registration
        if (isColliding && Time.time >= hitTimer + hitCooldown)
        {
            renderer.material.color = originalColor;
            isColliding = false;
        }
    }

    // This method is called when a collision occurs with the enemy's collider
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a Rigidbody component and if the cooldown period has passed
        Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
        if (otherRb != null && Time.time >= hitTimer + hitCooldown)
        {
            // If the colliding object has a Rigidbody, increment the hit count and start the cooldown timer
            hitCount++;
            hitTimer = Time.time;

            // If the hit count is greater than or equal to 10, destroy the enemy
            if (hitCount >= 10)
            {
                Destroy(gameObject);
            }
            else
            {
                // Otherwise, change the color of the enemy to red and set isColliding to true
                renderer.material.color = Color.red;
                isColliding = true;
            }
        }
    }
}
