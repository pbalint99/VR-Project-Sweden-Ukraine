using EzySlice;
using UnityEngine;
using System.Collections;


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
        return obj.Slice(transform.position, transform.up, mat);
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
        //stroy(obj, 3f);
    }



}
