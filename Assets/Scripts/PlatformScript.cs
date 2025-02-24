using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Transform dirArrow;
    public PlatformSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        dirArrow.rotation = Quaternion.Euler(0, 180, Random.Range(0f, 180f));
    }

    private void OnEnable()
    {
        dirArrow.rotation = Quaternion.Euler(0, 180, Random.Range(0f, 180f));
    }

    private void OnDisable()
    {
        spawner.SetSpawn();
    }

    public float GetDirection()
    {
        return dirArrow.rotation.eulerAngles.z;
    }
}
