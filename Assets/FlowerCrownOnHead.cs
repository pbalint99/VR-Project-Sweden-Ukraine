using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FlowerCrownOnHead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Wreath of flowers")
        {
            GetComponent<MeshRenderer>().enabled = true;
            Destroy(other.gameObject);
            this.gameObject.AddComponent<Throwable>();
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        
    }
}
