using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public Boy boy;
    public GameObject lock_;
    private bool canOpenDoor = false;

    public GameObject OpenDoorTextBox;

    private void OnTriggerEnter(Collider other)
    {
        canOpenDoor = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canOpenDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canOpenDoor || boy.HasBall())
        {
            canOpenDoor = false;
            OpenDoorTextBox.SetActive(false);
        }

        if (canOpenDoor && Boy.hasKey) {

            OpenDoorTextBox.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                leftDoor.Rotate(0f, -90f, 0f);
                rightDoor.Rotate(0f, 90f, 0f);
                canOpenDoor = false;
                lock_.SetActive(false);
                Boy.hasKey = false;
                OpenDoorTextBox.SetActive(false);
            }
        }
    }
}
