using UnityEngine;

public class Boy : MonoBehaviour
{
    public float PUSH_FORCE;
    
    public static bool hasKey = false;

    //Text boxes
    public GameObject grabBallTextBox;
    public GameObject dropBallTextBox;
    public GameObject hardPushTextBox;

    public GameObject ballCollider;
    public Ball ball;

    private bool hasBall = false;
    private bool ballInRange;

    private void Start()
    {
        ReleaseBall();
    }

    private void Update()
    {
        if (hasBall)
        {
            if (Input.GetKeyDown(KeyCode.E) || !ballInRange)
                ReleaseBall();

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ReleaseBall();
                ball.transform.GetComponent<Rigidbody>().AddForce(transform.forward * PUSH_FORCE);
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
        ballCollider.SetActive(true);
        ballCollider.transform.position = 
            new Vector3(transform.position.x, ball.transform.position.y, transform.position.z) + 
                transform.forward * ball.DISTANCE_TO_BOY;
        ball.OnGrab();
    }

    public void ReleaseBall()
    {
        hasBall = false;
        ball.OnRelease();
        ballCollider.SetActive(false);
    }

    public bool HasBall()
    {
        return hasBall;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBall && other.transform.CompareTag("Ball"))
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
        if (other.gameObject.CompareTag("Ball"))
        {
            grabBallTextBox.SetActive(false);
            ballInRange = false;
        }
    }
}
