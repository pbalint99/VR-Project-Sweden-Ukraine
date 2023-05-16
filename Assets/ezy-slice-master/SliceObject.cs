using EzySlice;
using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;


public class SliceObject : MonoBehaviour {
    public Material materialSlicedSide;
    public float explosionForce;
    public float exposionRadius;
    public bool gravity, kinematic;

    private bool canSlice = true;

    private IEnumerator ResetCanSlice()
    {
        yield return new WaitForSeconds(0.3f);
        canSlice = true;
    }



    private void OnTriggerEnter(Collider other) {
        if (canSlice && other.gameObject.CompareTag("CanSlice")) {
            canSlice = false;
            StartCoroutine(ResetCanSlice());
            
            SlicedHull sliceobj = Slice(other.gameObject, materialSlicedSide);

            GameObject SlicedObjTop = sliceobj.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject SlicedObjDown = sliceobj.CreateLowerHull(other.gameObject, materialSlicedSide);
            Destroy(other.gameObject);
            AddComponent(SlicedObjTop);
            AddComponent(SlicedObjDown);
        }
    }


    private SlicedHull Slice(GameObject obj, Material mat)
    {
        Quaternion originalRotation = transform.rotation;
        transform.rotation *= Quaternion.Euler(90f, 0f, 0f); // Rotate 90 degrees around the X-axis
        SlicedHull slicedHull = obj.Slice(transform.position, transform.up, mat);
        transform.rotation = originalRotation; // Reset the rotation to the original state
        return slicedHull;
    }

    void AddComponent(GameObject obj) {
        obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.position = transform.position;
        //rigidbody.useGravity = gravity;
        rigidbody.useGravity = true;
        rigidbody.isKinematic = kinematic;
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, exposionRadius);
        obj.tag = "CanSlice";
        obj.layer = 6; //Grabbable
        obj.AddComponent<Throwable>();
        //stroy(obj, 3f);
    }



}
