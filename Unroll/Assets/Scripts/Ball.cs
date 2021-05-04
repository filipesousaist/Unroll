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

    public void ChangeColor(ElementalColor newColor)
    {
        blocksManager.ChangeBreakableBlocks(color, newColor);
        myRenderer.material = newColor.ballMaterial;
        color = newColor;
    }

    public void OnGrab()
    {
        //joint.connectedBody = colliderRigidbody;
        //joint.xDrive = joint.yDrive = joint.zDrive = DRIVE_ACTIVE;
        transform.position = colliderRigidbody.transform.position;
        CreateJoint();
        //myRigidbody.mass = 0.2f;
        //myRigidbody.rotation = Quaternion.identity;
        myRigidbody.velocity = Vector3.zero;
    }

    public void OnRelease()
    {
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

    private void Update()
    {
        if (myRigidbody.velocity.magnitude < 1e-4)
            myRigidbody.rotation = Quaternion.identity;
    }
}
