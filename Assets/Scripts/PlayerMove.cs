using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator animator;

    public Rigidbody leftHand;
    public Rigidbody rightHand;
    [SerializeField] Rigidbody curHand;

    public GameObject leftShoulder;
    public GameObject rightShoulder;
    GameObject curShoulder;

    public GameObject rotAxis;
    public ConfigurableJoint movePoint;
    public ConfigurableJoint sword;

    public float dist;

    Vector3 lHandPos;
    Vector3 rHandPos;
    Vector3 curHandPos;
    public Transform ss;

    public float moveSpeed;
    float mouseX;
    float mouseY;

    float axisDist;
    float shortDist;
    float longDist;

    bool verticalMode = false;
    bool isLhand;
    bool grabbed = false;
    bool isSwordAncherShouldLong = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lHandPos = leftHand.transform.position;
        rHandPos = rightHand.transform.position;
        curHand = leftHand;
        curShoulder = leftShoulder;
        isLhand = true;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        //Debug.Log("X: " + mouseX + " Y: " +  mouseY);

        if (Input.GetKeyDown(KeyCode.Alpha1) && isLhand == false)
        {
            ChangeHand(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && isLhand == true)
        {
            ChangeHand(false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            verticalMode = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            verticalMode = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!grabbed)
            {
                if (sword == null && curHand.GetComponent<CollisionChecker>().GetCollision() != null)
                {
                    sword = curHand.GetComponent<CollisionChecker>().GetCollision().GetComponent<ConfigurableJoint>();
                    ChangeHand(!isLhand);
                }
                else if (sword != null && curHand.GetComponent<CollisionChecker>().GetCollision() == sword.gameObject)
                {
                    MakeAxis();
                }
            }
        }

        curHandPos.x += mouseX * moveSpeed * Time.deltaTime;

        if (verticalMode == true)
        {
            curHandPos.y += mouseY * moveSpeed * Time.deltaTime;
        }
        else
        {
            curHandPos.z += mouseY * moveSpeed * Time.deltaTime;
        }

        if (grabbed)
        {
            curHandPos.x = Mathf.Clamp(curHandPos.x, rotAxis.transform.position.x - axisDist, rotAxis.transform.position.x + axisDist);
            curHandPos.y = Mathf.Clamp(curHandPos.y, rotAxis.transform.position.y - axisDist, rotAxis.transform.position.y + axisDist);
            curHandPos.z = Mathf.Clamp(curHandPos.z, rotAxis.transform.position.z - axisDist, rotAxis.transform.position.z + axisDist);
        }

        curHandPos.x = Mathf.Clamp(curHandPos.x, curShoulder.transform.position.x - dist, curShoulder.transform.position.x + dist);
        curHandPos.y = Mathf.Clamp(curHandPos.y, curShoulder.transform.position.y - dist, curShoulder.transform.position.y + dist);
        curHandPos.z = Mathf.Clamp(curHandPos.z, curShoulder.transform.position.z - dist, curShoulder.transform.position.z + dist);

        ss.transform.position = curHandPos;
        if (!isLhand)
        {
            leftHand.transform.position = lHandPos;
        }
        else
        {
            rightHand.transform.position = rHandPos;
        }
        if (grabbed)
        {
            //curHandPos = movePoint.transform.position;
            movePoint.transform.position = curHand.transform.position;
            //if (sword.GetComponent<Rigidbody>().velocity.magnitude >= 20)
            //{
            //    Debug.Log("Sword magni: " + sword.GetComponent<Rigidbody>().velocity.magnitude); //20쯤?
            //}
        }
        curHand.transform.position = curShoulder.transform.position + Vector3.ClampMagnitude(curHandPos - curShoulder.transform.position, dist);
    }

    void MakeAxis()
    {
        grabbed = true;
        Debug.Log("MakeAxis");
        axisDist = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
        if (isLhand)
        {
            rotAxis.transform.parent = rightHand.transform;
            rotAxis.transform.position = rightHand.transform.position;
            movePoint.transform.parent = leftHand.transform;
            movePoint.transform.position = leftHand.transform.position;
        }
        else
        {
            rotAxis.transform.parent = leftHand.transform;
            rotAxis.transform.position = leftHand.transform.position;
            movePoint.transform.parent = rightHand.transform;
            movePoint.transform.position = rightHand.transform.position;
        }
        movePoint.connectedBody = rotAxis.GetComponent<Rigidbody>();
        sword.autoConfigureConnectedAnchor = false;
        sword.connectedAnchor = Vector3.zero;
        sword.connectedBody = movePoint.GetComponent<Rigidbody>();
        //sword.GetComponent<ConfigurableJoint>().anchor = new Vector3(0, -axisDist, 0);
        if (rotAxis.transform.position.y > movePoint.transform.position.y) //축이 위인가?
        {
            isSwordAncherShouldLong = false;
            shortDist = Vector3.Distance(sword.transform.position, rotAxis.transform.position);
            longDist = Vector3.Distance(sword.transform.position, movePoint.transform.position);
            movePoint.anchor = new Vector3(0, axisDist, 0);
            sword.anchor = new Vector3(0, -longDist, 0);
        }
        else
        {
            isSwordAncherShouldLong = true;
            shortDist = Vector3.Distance(sword.transform.position, movePoint.transform.position);
            longDist = Vector3.Distance(sword.transform.position, rotAxis.transform.position);
            movePoint.anchor = new Vector3(0, -axisDist, 0);
            sword.anchor = new Vector3(0, -shortDist, 0);
        }
        sword.GetComponent<Slicer>().enabled = true;
        //sword.GetComponent<ConfigurableJoint>().anchor = Vector3.zero;
        //sword.GetComponent<ConfigurableJoint>().connectedAnchor = new Vector3(0, Vector3.Distance(rotAxis.transform.position, movePoint.transform.position) / sword.transform.localScale.y, 0);
        //sword.GetComponent<ConfigurableJoint>().connectedAnchor = Vector3.zero;
        //sword.GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Locked;
        //sword.GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Locked;
        //sword.GetComponent<ConfigurableJoint>().zMotion = ConfigurableJointMotion.Locked;
    }

    void ChangeHand(bool lHand)
    {
        if (lHand == true && isLhand == false)
        {
            isLhand = true;
            rHandPos = rightHand.transform.position;
            curHandPos = lHandPos;
            curHand = leftHand;
            curShoulder = leftShoulder;
            if (grabbed)
            {
                rotAxis.transform.parent = rightHand.transform;
                rotAxis.transform.position = rightHand.transform.position;
                movePoint.transform.parent = leftHand.transform;
                movePoint.transform.position = leftHand.transform.position;
            }
        } 
        else if (lHand == false && isLhand == true)
        {
            isLhand = false;
            lHandPos = leftHand.transform.position;
            curHandPos = rHandPos;
            curHand = rightHand;
            curShoulder = rightShoulder;
            if (grabbed)
            {
                rotAxis.transform.parent = leftHand.transform;
                rotAxis.transform.position = leftHand.transform.position;
                movePoint.transform.parent = rightHand.transform;
                movePoint.transform.position = rightHand.transform.position;
            }
        }

        if (grabbed)
        {
            movePoint.connectedBody = null;
            sword.connectedBody = null;
            if (isSwordAncherShouldLong)
            {
                movePoint.anchor = new Vector3(0, axisDist, 0);
                sword.anchor = new Vector3(0, -longDist, 0);
            }
            else
            {
                movePoint.anchor = new Vector3(0, -axisDist, 0);
                sword.anchor = new Vector3(0, -shortDist, 0);
            }
            movePoint.connectedBody = rotAxis.GetComponent<Rigidbody>();
            sword.connectedBody = movePoint.GetComponent<Rigidbody>();
            isSwordAncherShouldLong = !isSwordAncherShouldLong;
        }
        //movePoint.GetComponent<ConfigurableJoint>().anchor = new Vector3(0, -movePoint.GetComponent<ConfigurableJoint>().anchor.y, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftShoulder.transform.position, dist);
        Gizmos.DrawWireSphere(rightShoulder.transform.position, dist);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(leftShoulder.transform.position, new Vector3(dist * 2, dist * 2, dist * 2));
        Gizmos.DrawWireCube(rightShoulder.transform.position, new Vector3(dist * 2, dist * 2, dist * 2));
    }
}
