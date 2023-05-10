using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyllinderCollision : MonoBehaviour
{
  public GameObject torchFire;

    private void OnTriggerEnter(Collider other)

    {
        Debug.Log("Collision detected");
        if (other.gameObject.name == "Fire")
        {
                    Debug.Log("Collision detected");

            torchFire.SetActive(true);
        }
    }
    
}
