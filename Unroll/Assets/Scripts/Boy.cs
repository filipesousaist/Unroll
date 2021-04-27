using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public float PUSH_FORCE;
    public float DISTANCE;
    
    [SerializeField]
    Transform Ball;

    private bool ballInRange;
    private Transform tempBallTransform;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 vectorStart = transform.position + new Vector3(0, 0.5f, 0);

        bool ballCollision = Physics.Raycast(vectorStart, transform.forward, out hit, 1.5f);
        

        if (Ball != null)
        {
            BallPositionUpdate();

            if (Input.GetKeyDown(KeyCode.E) || !ballInRange)
            {
                Ball = null;
            }

            else if (Input.GetKey(KeyCode.LeftShift))
            {
                Ball.GetComponent<Rigidbody>().AddForce(transform.forward * PUSH_FORCE);
                Ball = null;
            }

        }

        else if (Input.GetKeyDown(KeyCode.E))
        {

            if (ballInRange)
            {
                Ball = tempBallTransform.transform;
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void BallPositionUpdate()
    {
        Ball.position = new Vector3(transform.position.x + transform.forward.x * DISTANCE, Ball.position.y, transform.position.z + transform.forward.z * DISTANCE);
        Ball.GetComponent<Rigidbody>().angularVelocity = Ball.GetComponent<Rigidbody>().angularVelocity.normalized * Ball.GetComponent<Rigidbody>().velocity.magnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (Ball == null && other.transform.CompareTag("Ball"))
        {
            ballInRange = true;
            tempBallTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Ball"))
        {
            ballInRange = false;

        }
    }
}
