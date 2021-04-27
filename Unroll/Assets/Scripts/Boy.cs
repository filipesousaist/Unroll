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
            if (Input.GetKeyDown(KeyCode.E) || !ballCollision || !hit.transform.CompareTag("Ball"))
                
                Ball = null;

            else if (Input.GetKey(KeyCode.LeftShift))
            {
                Ball.GetComponent<Rigidbody>().AddForce(transform.forward * PUSH_FORCE);
                Ball = null;
            }

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {

            if ((ballCollision ||
                Physics.Raycast(vectorStart, Quaternion.AngleAxis(15, Vector3.up) * transform.forward, out hit, 1.5f) ||
                Physics.Raycast(vectorStart, Quaternion.AngleAxis(-15, Vector3.up) * transform.forward, out hit, 1.5f))
                && hit.transform.CompareTag("Ball"))
            {
                Ball = hit.transform;
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Ball != null)
            BallPositionUpdate();
    }

    void BallPositionUpdate()
    {
        Ball.position = new Vector3(transform.position.x + transform.forward.x * DISTANCE, Ball.position.y, transform.position.z + transform.forward.z * DISTANCE);
    }

}
