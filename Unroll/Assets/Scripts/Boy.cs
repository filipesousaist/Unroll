using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public float SPEED;
    public float ANGULAR_SPEED;
    public float DISTANCE;
    
    [SerializeField]
    Transform Ball;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;

        float rotY = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;

        if (Input.GetKey(KeyCode.W))
            myRigidbody.velocity += new Vector3(SPEED * Mathf.Cos(rotY), 0, -SPEED * Mathf.Sin(rotY));
        if (Input.GetKey(KeyCode.S))
            myRigidbody.velocity += new Vector3(-SPEED * Mathf.Cos(rotY), 0, SPEED * Mathf.Sin(rotY));
        if (Input.GetKey(KeyCode.A))
            myRigidbody.angularVelocity += new Vector3(0, -ANGULAR_SPEED, 0);
        if (Input.GetKey(KeyCode.D))
            myRigidbody.angularVelocity += new Vector3(0, ANGULAR_SPEED, 0);

        if (Ball != null)
        {
            BallPositionUpdate();
        }
    }

    void BallPositionUpdate()
    {
        Ball.position = new Vector3(transform.position.x + transform.right.x * DISTANCE, Ball.position.y, transform.position.z + transform.right.z * DISTANCE);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Ball"))
        {
            //if (Input.GetKey(KeyCode.LeftShift))
                Ball = collision.collider.transform;
        }
    }
}
