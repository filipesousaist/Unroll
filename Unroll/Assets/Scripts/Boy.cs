using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public float PUSH_FORCE;
    public float DISTANCE;

    public static bool hasKey = false;

    //Text boxes
    public GameObject grabBallTextBox;
    public GameObject dropBallTextBox;
    public GameObject hardPushTextBox;
    
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
        if (!PauseMenu.GameIsPaused)
        {

            RaycastHit hit;
            Vector3 vectorStart = transform.position + new Vector3(0, 0.5f, 0);

            bool ballCollision = Physics.Raycast(vectorStart, transform.forward, out hit, 1.5f);

            if (Ball == null)
            {
                dropBallTextBox.SetActive(false);
                hardPushTextBox.SetActive(false);
            }

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
                    grabBallTextBox.SetActive(false);
                    dropBallTextBox.SetActive(true);
                    hardPushTextBox.SetActive(true);
                    Ball = tempBallTransform.transform;
                }
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

    public bool HasBall()
    {
        return Ball != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (Ball == null && other.transform.CompareTag("Ball"))
        {
            grabBallTextBox.SetActive(true);
            ballInRange = true;
            tempBallTransform = other.transform;
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
        if (other.transform.CompareTag("Ball"))
        {
            grabBallTextBox.SetActive(false);
            ballInRange = false;

        }
    }

    public bool CheckIfHasKey() {
        return hasKey;
    }
}
