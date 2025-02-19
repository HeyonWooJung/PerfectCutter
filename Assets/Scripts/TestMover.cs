using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMover : MonoBehaviour
{
    public Transform target;
    public Transform target2;
    ConfigurableJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * 5, Space.World);
            //transform.position = new Vector3(-2, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * 5, Space.World);
            //transform.position = new Vector3(2, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5, Space.World);
            //transform.position = new Vector3(0, 0, 2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * 5, Space.World);
            //transform.position = new Vector3(0, 0, -2);
        }*/
        //joint.targetPosition = target.position;
        //joint.targetVelocity = (target.position - joint.transform.position).normalized;
        target.LookAt(target2, Vector3.up);
        joint.targetRotation = target.rotation;
    }
}
