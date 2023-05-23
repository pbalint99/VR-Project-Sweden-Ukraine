using System.Collections.Generic;
using UnityEngine;

public class Bonfirelit : MonoBehaviour
{
    public GameObject bonFire;

    private void OnTriggerEnter(Collider other)

    {
        Debug.Log("Collision detected");
        if (other.gameObject.name.Contains("torch"))
        {
                    //Debug.Log("Collision detected");

            bonFire.SetActive(true);
        }
    }
}