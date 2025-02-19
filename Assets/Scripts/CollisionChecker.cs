using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    bool isColliding;
    GameObject collisioned;
    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
        collisioned = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
            isColliding = true;
        if (collision.gameObject.CompareTag("Sword"))
        {
            collisioned = collision.gameObject;
        }
        else
        {
            collisioned = null;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
        collisioned = null;
    }

    public GameObject GetCollision()
    {
        Debug.Log("GC: " + collisioned?.name);
        if (collisioned == null)
        {
            return null;
        }
        return collisioned;
    }
}
