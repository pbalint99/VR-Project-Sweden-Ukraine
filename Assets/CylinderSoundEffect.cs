using UnityEngine;

public class CylinderSoundEffect : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            //Debug.Log("Capsule collided with Cylinder");
            audioSource.Play();
        }
    }
}
