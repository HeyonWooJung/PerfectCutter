using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using System;

public class Slicer : MonoBehaviour
{
    [SerializeField] Material slicedMat;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] LayerMask sliceableLayer;
    [SerializeField] Rigidbody rb;
    [SerializeField] float swingPower;
    [SerializeField] float angleLimit;
    [SerializeField] Transform cuts;
    [SerializeField] AudioManager audioManager;
    int slicedLayer;

    public event Action<int> OnScoreChanged;

    void Start()
    {
        audioManager.EquipSword();
        slicedLayer = LayerMask.NameToLayer("Sliceable");
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude >= swingPower && !audioManager.Playing())
        {
            audioManager.SwingSword();
        }
        if (Physics.Linecast(startPoint.position, endPoint.position, out RaycastHit hit, sliceableLayer) && rb.velocity.magnitude >= swingPower)
        {
            Slice(hit.transform.gameObject);
        }
    }

    public void PrecisionSetting(float sPower, float aLimit)
    {
        swingPower = sPower;
        angleLimit = aLimit;
    }

    public void Slice(GameObject target)
    {
        Vector3 planeVector = Vector3.Cross(endPoint.position - startPoint.position, rb.velocity);
        planeVector.Normalize();

        float arrowAngle = target.GetComponent<PlatformScript>().GetDirection();
        float swordAngle = Vector3.Angle(endPoint.position.normalized, planeVector);
        Debug.Log("Angle: " + swordAngle);

        if (arrowAngle - angleLimit <= swordAngle && swordAngle <= arrowAngle + angleLimit)
        {
            SlicedHull hull = target.Slice(endPoint.position, planeVector);

            if (hull != null)
            {
                GameObject upperHull = hull.CreateUpperHull(target, slicedMat);
                GameObject lowerHull = hull.CreateLowerHull(target, slicedMat);

                upperHull.transform.SetParent(cuts);
                lowerHull.transform.SetParent(cuts);
                target.SetActive(false);

                SetSlicedComponent(upperHull);
                SetSlicedComponent(lowerHull);

                /*float upperDist = Vector3.Distance(target.transform.position, upperHull.GetComponent<Collider>().bounds.center);
                float lowerDist = Vector3.Distance(target.transform.position, lowerHull.GetComponent<Collider>().bounds.center);
                if (target.GetComponent<Rigidbody>().isKinematic == true)
                {
                    if (upperDist < lowerDist)
                    {
                        upperHull.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    else
                    {
                        lowerHull.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }*/
            }
            int score = 100 - (int)MathF.Abs(arrowAngle - swordAngle);
            NotifyScoreChange(score);
            audioManager.BreakWood(score);
        }
        else
        {
            NotifyScoreChange(-(int)MathF.Abs(arrowAngle - swordAngle));
        }
    }

    void SetSlicedComponent(GameObject sliced)
    {
        Rigidbody sRb = sliced.AddComponent<Rigidbody>();
        MeshCollider sliceMesh = sliced.AddComponent<MeshCollider>();
        sliced.layer = slicedLayer;
        sliceMesh.convex = true;
        sRb.AddExplosionForce(2000f, sliced.transform.position, 0.01f);
        Destroy(sliced, 2f);
    }

    public void NotifyScoreChange(int score)
    {
        if (OnScoreChanged != null)
        {
            OnScoreChanged(score);
        }
    }
}
