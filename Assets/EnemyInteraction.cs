using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyInteraction : MonoBehaviour {
    //private Renderer renderer;
    private Color originalColor;
    //private bool isColliding = false;
    private int hitCount = 0;
    public int hitsToDie = 20;
    private float hitCooldown = 1f;
    private float hitTimer = 0.0f;
    public ParticleSystem particleSystem;
    Animator animator;
    public float movementSpeed = 5.0f;
    private bool isWalking = true;
    bool isHit = false;
    public Image heartFill;
    public GameObject player;
    public float distanceToNoticePlayer;
    public float rotationSpeed = 5.0f;
    bool facingPlayer = false;

    public GameObject goal1;
    public GameObject goal2;

    int goalnumber = 1;

    Transform goal;

    bool isScreaming;
    bool hasScreamed = false;
    bool playerIsClose = false;
    bool isDancing = false;

    public GameObject flowerCrown;
    public GameObject hat;

    public AudioClip danceMusic;
    public AudioClip scream;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        // Get the Renderer component and original color from the enemy object
        //renderer = GetComponent<Renderer>();
        //originalColor = renderer.material.color;
        animator = GetComponentInChildren<Animator>();
        goal = goal1.transform;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        // if(!isWalking && Input.GetKeyDown(KeyCode.KeypadEnter)) {
        //     isWalking = true;
        // }
        if(isDancing) return;

        if(isWalking) {
            // Set the "isWalking" parameter of the animator to true
            animator.SetBool("isWalking", true);
            Walk();
            if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
                isWalking = false;
                animator.SetBool("isWalking", false);
            }
        }
        if(isScreaming) {
            // Set the "isWalking" parameter of the animator to true
            StartCoroutine(Scream());
        }

        //If the player is close face them
        float distance = (player.transform.position - gameObject.transform.position).magnitude;
        if(distance<distanceToNoticePlayer) {
            playerIsClose = true;
            if(!hasScreamed) {
                isScreaming = true;
                hasScreamed = true;
            }
            // Calculate the direction from the enemy to the player
            Vector3 direction = player.transform.position - transform.position;

            // Calculate the desired rotation of the enemy, only around the Y axis
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // Smoothly rotate the enemy toward the player using Lerp
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        } else {
            playerIsClose = false;
        }

        if(Input.GetKeyDown(KeyCode.K)) {
            hitCount = hitsToDie;
        }
        
    }

    // This method is called when a collision occurs with the enemy's collider
    private void OnCollisionEnter(Collision collision) {
        if (isDancing) return;
        int otherLayer = collision.gameObject.layer;
        string name = collision.gameObject.name;

        if(collision.gameObject.tag == "Flower") {
            isDancing = true;
            animator.SetBool("isDancing",true);
            flowerCrown.SetActive(true);
            hat.SetActive(false);
            Destroy(collision.gameObject);
            // Play the audio clip
            audioSource.clip = danceMusic;
            audioSource.time = 11f;
            audioSource.Play();
            return;
        }

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
            if(name == "HandColliderRight(Clone)" || name == "HandColliderLeft(Clone)")
                hitCount++;
            else {
                hitCount += 2;
            }
            hitTimer = Time.time;
            StartCoroutine(ManageHitEffects());
        }
    }

    private IEnumerator ManageHitEffects() {
        particleSystem.Play();
        animator.SetBool("isHit", true);
        isHit = true;
        isWalking = false;
        heartFill.fillAmount = 1 - (float)hitCount / hitsToDie;
        yield return new WaitForSeconds(1f);
        animator.SetBool("isHit", false);
        particleSystem.Stop();
        particleSystem.Clear();
        // If the hit count is greater than or equal to hitsToDie, destroy the enemy
        if (hitCount >= hitsToDie)
        {
            Destroy(this.gameObject);
        }
        isWalking = true;
    }

    private IEnumerator Scream() {
        animator.SetBool("isScreaming", true);
        //yield return new WaitForSeconds(0.25f);
        // Play the audio clip
        audioSource.clip = scream;
        audioSource.Play();

        yield return new WaitForSeconds(3.15f);
        animator.SetBool("isScreaming", false);
        isScreaming = false;
        isWalking = true;
    }

    private void Walk() 
    {
        if(!playerIsClose) {
            // Move the character forward in the direction it is facing
            Vector3 direction = -(transform.position - goal.position).normalized;
            transform.position += new Vector3(direction.x, 0, direction.z) * movementSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // Smoothly rotate the enemy toward the player using Lerp
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if(new Vector2(transform.position.x - goal.position.x, transform.position.z - goal.position.z).magnitude < 0.5) {
                if (goalnumber == 1) {
                    goal = goal2.transform;
                    goalnumber = 2;
                }
                else {
                    goal = goal1.transform;
                    goalnumber = 1;
                }
            }
        }
        else {
            Vector3 direction = -(transform.position - player.transform.position).normalized;
            transform.position += new Vector3(direction.x, 0, direction.z) * movementSpeed * Time.deltaTime;
        }

    }

}