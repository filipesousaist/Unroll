using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ElementalColor color;

    public float DISTANCE_TO_BOY;
    public float STICK_THRESHOLD;
    public float RELEASE_THRESHOLD;

    public Rigidbody colliderRigidbody;
    public Boy boy;

    private MeshRenderer myRenderer;
    private Rigidbody myRigidbody;
    private SphereCollider myCollider;

    private BlocksManager blocksManager;

    private Vector3 lastPosition;
    private float RADIUS;
    
    void Awake()
    {
        myRenderer = GetComponent<MeshRenderer>();
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<SphereCollider>();
        blocksManager = FindObjectOfType<BlocksManager>();
    }

    private void Start()
    {
        lastPosition = transform.position;
        RADIUS = transform.localScale.y / 2;
    }

    private void Update()
    {
        if (myRigidbody.isKinematic)
        {

            UpdatePosition();
            UpdateRotation();
        }
    }

    private void UpdatePosition()
    {
        lastPosition = transform.position;

        transform.position = new Vector3(
            boy.transform.position.x + boy.transform.forward.x * DISTANCE_TO_BOY,
            colliderRigidbody.transform.position.y,
            boy.transform.position.z + boy.transform.forward.z * DISTANCE_TO_BOY
        );

        CheckDistanceToFloor();
    }

    private void CheckDistanceToFloor()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit downHit, LayerMask.GetMask("Default")))
        {
            if (downHit.distance > RADIUS + RELEASE_THRESHOLD)
                boy.ReleaseBall();
            else if (downHit.distance >= RADIUS && downHit.distance <= RADIUS + STICK_THRESHOLD)
                StickToFloor(downHit);      
        }
    }

    private void StickToFloor(RaycastHit downHit)
    {
        transform.position += Vector3.down * (downHit.distance - RADIUS);
        if (Physics.Raycast(transform.position, -downHit.normal, out RaycastHit normalHit, RADIUS + STICK_THRESHOLD, LayerMask.GetMask("Default")))
            colliderRigidbody.velocity = downHit.normal * (normalHit.distance - RADIUS) / Time.deltaTime;
    }

    private void UpdateRotation()
    {
        Vector3 difference = transform.position - lastPosition;
        Vector3 cross = Vector3.Cross(Vector3.up, difference.normalized);

        transform.Rotate(cross, difference.magnitude * Mathf.Rad2Deg / RADIUS, Space.World);
    }

    public void ChangeColor(ElementalColor newColor)
    {
        blocksManager.ChangeBreakableBlocks(color, newColor);
        myRenderer.material = newColor.ballMaterial;
        color = newColor;
    }

    public void OnGrab()
    {
        myRigidbody.velocity = Vector3.zero; // So that on release the velocity is forgotten
        myRigidbody.isKinematic = true;
        myCollider.enabled = false;
        transform.position = colliderRigidbody.transform.position;
    }

    public void OnRelease()
    {
        myRigidbody.isKinematic = false;
        myCollider.enabled = true;
    }
}
