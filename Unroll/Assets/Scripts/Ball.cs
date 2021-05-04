using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM.Walkthrough.OverShoulderCamera;
using ECM.Components;

public class Ball : MonoBehaviour
{
    public ElementalColor color;

    private MeshRenderer myRenderer;

    private BlocksManager blocksManager;

    public Transform ecmTransform;

    public Rigidbody ballRigidbody;

    public Rigidbody ecmRigidbody;

    public CapsuleCollider ecmCollider;

    public SphereCollider ballCollider;

    public MyCharacterController myCharacterController;

    public CharacterMovement characterMovement;

    public Camera ballCam;

    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        blocksManager = FindObjectOfType<BlocksManager>();
        Debug.Log(this.gameObject.transform.position);
    }

    public void ChangeColor(ElementalColor newColor)
    {
        blocksManager.ChangeBreakableBlocks(color, newColor);
        myRenderer.material = newColor.ballMaterial;
        color = newColor;
    }

    public void ActivateControl() {
        ballRigidbody.isKinematic = true;
        ecmRigidbody.isKinematic = false;
        ecmCollider.enabled = true;
        ballCollider.enabled = false;
        myCharacterController.enabled = true;
        characterMovement.enabled = true;
        var newPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y * -1f, this.gameObject.transform.position.z);
        ecmTransform.position = newPos;
        ecmTransform.rotation = Quaternion.identity;
        ballCam.gameObject.SetActive(true);
        this.gameObject.transform.localPosition = new Vector3(0, 1f, 0);
    }

    public void DeactivateControl() {
        ballRigidbody.isKinematic = false;
        ecmRigidbody.isKinematic = true;
        ecmCollider.enabled = false;
        ballCollider.enabled = true;
        myCharacterController.enabled = false;
        characterMovement.enabled = false;
        ballCam.gameObject.SetActive(false);
    }
}
