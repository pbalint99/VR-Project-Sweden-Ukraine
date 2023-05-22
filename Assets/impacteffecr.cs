using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impacteffecr : MonoBehaviour
{
    public ParticleSystem hitParticleSystem;

    void Start()
    {
        hitParticleSystem.Stop();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision detected");
        // Check if the collision occurred with the woods
        if (collision.gameObject.CompareTag("CanSlice"))
        {
            hitParticleSystem.Play();
            StartCoroutine(StopEffect());
        }
    }

    IEnumerator StopEffect()
    {
        yield return new WaitForSeconds(0.4f);
        hitParticleSystem.Stop();
    }
}
