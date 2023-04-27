using UnityEngine;

public class CylinderSoundEffect : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Capsule")
        {
            //Debug.Log("Capsule collided with Cylinder");
            audioSource.Play();
        }
    }
}
