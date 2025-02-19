using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceTester : MonoBehaviour
{
    [SerializeField] GameObject toSlice;
    [SerializeField] Material mat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Slice(toSlice);
        }
    }

    public void Slice(GameObject target)
    {
        SlicedHull hull = target.Slice(transform.position, transform.up);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, mat);
            GameObject lowerHull = hull.CreateLowerHull(target, mat);

            target.SetActive(false);

            SetSlicedComponent(upperHull);
            SetSlicedComponent(lowerHull);
        }
    }

    void SetSlicedComponent(GameObject sliced)
    {
        Rigidbody sRb = sliced.AddComponent<Rigidbody>();
        MeshCollider sliceMesh =  sliced.AddComponent<MeshCollider>();
        sliceMesh.convex = true;

        sRb.AddForce(transform.up.normalized * 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        toSlice = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        toSlice = null;
    }
}
