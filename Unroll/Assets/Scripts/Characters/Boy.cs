using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

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
            {
                ReleaseBall();
                ReportPressE(false);
            }

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
                ReportPressE(true);
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

    public bool CheckIfHasKey() {
        return hasKey;
    }

    private void ReportPressE(bool ballGrabed)
    {
        AnalyticsEvent.Custom("press_E", new Dictionary<string, object>{
            { "ball_grabed", ballGrabed }
        });
    }
}
