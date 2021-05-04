using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    public float PUSH_FORCE;
    public float DISTANCE;

    public static bool hasKey = false;

    //Text boxes
    public GameObject grabBallTextBox;
    public GameObject dropBallTextBox;
    public GameObject hardPushTextBox;

    public Rigidbody ballColliderRigidbody;
    private HingeJoint joint;
    //private ConfigurableJoint joint;

    private bool hasBall = false;
    private Ball ball;
    private bool ballInRange;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
    }
    private void Start()
    {
        ReleaseBall();
    }

    private void Update()
    {
        if (hasBall)
        {
            //BallPositionUpdate();

            if (Input.GetKeyDown(KeyCode.E) || !ballInRange)
                ReleaseBall();

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ball.transform.GetComponent<Rigidbody>().AddForce(transform.forward * PUSH_FORCE);
                ReleaseBall();
            }
        }
        else // Not grabbing ball
        {
            dropBallTextBox.SetActive(false);
            hardPushTextBox.SetActive(false);

            if (Input.GetKeyDown(KeyCode.E) && ballInRange)
            {
                grabBallTextBox.SetActive(false);
                dropBallTextBox.SetActive(true);
                hardPushTextBox.SetActive(true);
                GrabBall();
            }
        }
    }

    private void GrabBall()
    {
        hasBall = true;

        ballColliderRigidbody.gameObject.SetActive(true);
        ballColliderRigidbody.velocity = Vector3.zero;

        CreateJoint();


        ball.OnGrab();
    }

    private void CreateJoint()
    {
        joint = gameObject.AddComponent<HingeJoint>();

        joint.connectedBody = ballColliderRigidbody;

        ballColliderRigidbody.transform.position = new Vector3(
            transform.position.x + transform.forward.x * DISTANCE,
            transform.position.y + 0.5f,
            transform.position.z + transform.forward.z * DISTANCE);


        //joint.autoConfigureConnectedAnchor = false;
        joint.anchor = transform.up * 0.5f; //-transform.forward * DISTANCE; //new Vector3(0, 0, -1);
        joint.connectedAnchor = Vector3.zero; //new Vector3(0, 0.5f, 0);

        joint.axis = Vector3.right; // new Vector3(-1, 0, 0);

        //joint.xMotion = joint.zMotion = ConfigurableJointMotion.Locked;
    }

    private void ReleaseBall()
    {
        hasBall = false;
        //joint.gameObject.SetActive(false);
        if (joint != null)
            Destroy(joint);

        ball.OnRelease();

        ballColliderRigidbody.gameObject.SetActive(false);
    }

    void BallPositionUpdate()
    {
        /*
        Transform ballTransform = ball.transform;
        ballTransform.position = new Vector3(transform.position.x + transform.forward.x * DISTANCE, ballTransform.position.y, transform.position.z + transform.forward.z * DISTANCE);
        ballTransform.GetComponent<Rigidbody>().angularVelocity = ballTransform.GetComponent<Rigidbody>().angularVelocity.normalized * ballTransform.GetComponent<Rigidbody>().velocity.magnitude;
        */   
    }

    public bool HasBall()
    {
        return hasBall;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (/*!hasBall && */other.transform.CompareTag("Ball") || other.transform.CompareTag("Ball Collider"))
        {
            grabBallTextBox.SetActive(true);
            ballInRange = true;
        }

        if (other.transform.CompareTag("Key"))
        {
            hasKey = true;
            other.gameObject.SetActive(false);
            Debug.Log("Key picked up");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Ball") || other.transform.CompareTag("Ball Collider"))
        {
            grabBallTextBox.SetActive(false);
            ballInRange = false;
        }
    }
}
