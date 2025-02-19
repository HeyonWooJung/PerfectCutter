using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    GameObject collisioned;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OCE: " + collision.transform.name);
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
