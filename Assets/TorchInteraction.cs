using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchinteraction : MonoBehaviour
{
  public GameObject bonFire;

    private void OnTriggerEnter(Collider other)

    {
        Debug.Log("Collision detected");
        if (other.gameObject.name == "torch")
        {
                    //Debug.Log("Collision detected");

            bonFire.SetActive(true);
        }
    }
    
}
