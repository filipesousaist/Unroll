using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBall : MonoBehaviour
{
    public Rigidbody colRigidbody;
    public float DISTANCE;
    public Transform boy;
    private Vector3 lastPosition;

    private float RADIUS;

    private void Start()
    {
        lastPosition = transform.position;
        RADIUS = transform.localScale.y / 2;
    }
    private void Update()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition()
    {
        lastPosition = transform.position;

        transform.position = new Vector3(boy.position.x + boy.forward.x * DISTANCE, colRigidbody.transform.position.y, boy.position.z + boy.forward.z * DISTANCE);

        StickToFloor();
    }

    private void StickToFloor2()
    {
        bool hasHit = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.5f, LayerMask.GetMask("Default"));

        if (hasHit)
        {
            colRigidbody.velocity = Vector3.down * (hit.distance - 0.5f) / Time.deltaTime;
            transform.position += Vector3.down * (hit.distance - 0.5f);
        }
            
    }

    private void StickToFloor()
    {
        // Determine normal to the floor
        bool hasHit1 = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit1, 0.5f + 1e-1f, LayerMask.GetMask("Default"));

        if (hasHit1)
        {
            transform.position += Vector3.down * (hit1.distance - 0.5f);
            // Determine distance to closest point in the floor
            bool hasHit2 = Physics.Raycast(transform.position, -hit1.normal, out RaycastHit hit2, 0.5f + 1e-1f, LayerMask.GetMask("Default"));
            if (hasHit2)
                colRigidbody.velocity = hit1.normal * (hit2.distance - 0.5f) / Time.deltaTime;
            
        }        
    }

    private void UpdateRotation()
    {
        Vector3 difference = transform.position - lastPosition;
        Vector3 cross = Vector3.Cross(Vector3.up, difference.normalized);

        transform.Rotate(cross, difference.magnitude * Mathf.Rad2Deg / RADIUS, Space.World);
    }

    
}
