using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ElementalColor color;

    public Rigidbody colliderRigidbody;
    private ConfigurableJoint joint; // Do not assign

    private Rigidbody myRigidbody;
    private MeshRenderer myRenderer;

    private BlocksManager blocksManager;

    private readonly JointDrive DRIVE_0 = new JointDrive() { positionSpring = 0, maximumForce = float.MaxValue };
    private readonly JointDrive DRIVE_ACTIVE = new JointDrive() { positionSpring = 200, maximumForce = float.MaxValue };

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRenderer = GetComponent<MeshRenderer>();
        blocksManager = FindObjectOfType<BlocksManager>();
    }

    private void Update()
    {
        if (myRigidbody.isKinematic)
            KinematicUpdate();
    }

    private void KinematicUpdate()
    {
        transform.position = colliderRigidbody.transform.position; // TODO: Is rigidbody necessary? Maybe only Transform
        // TODO: Rotation
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
        //joint.connectedBody = colliderRigidbody;
        //joint.xDrive = joint.yDrive = joint.zDrive = DRIVE_ACTIVE;
        //transform.position = colliderRigidbody.transform.position;
        //CreateJoint();
        //myRigidbody.mass = 0.2f;
        //myRigidbody.rotation = Quaternion.identity;
        
    }

    public void OnRelease()
    {
        myRigidbody.isKinematic = false;
        if (joint != null)
            Destroy(joint);
        //myRigidbody.mass = 2;
        //joint.connectedBody = null;
        //joint.xDrive = joint.yDrive = joint.zDrive = DRIVE_0;
    }

    private void CreateJoint()
    {
        joint = gameObject.AddComponent<ConfigurableJoint>();

        joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Locked;
        joint.xDrive = joint.yDrive = joint.zDrive = DRIVE_ACTIVE;

        //joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = colliderRigidbody;

        joint.anchor = Vector3.zero;
        joint.connectedAnchor = Vector3.zero;

        joint.enableCollision = false;

        joint.massScale = 2;

        
    }
}
