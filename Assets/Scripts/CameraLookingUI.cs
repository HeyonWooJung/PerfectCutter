using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookingUI : MonoBehaviour
{
    Transform mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.position);
    }
}
