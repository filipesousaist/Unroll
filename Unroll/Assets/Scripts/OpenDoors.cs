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
        if (Input.GetKeyDown(KeyCode.E) && canOpenDoor && boy.CheckIfHasKey())
        {
            leftDoor.Rotate(0f, -90f, 0f);
            rightDoor.Rotate(0f, 90f, 0f);
            canOpenDoor = false;
            lock_.SetActive(false);
        }
    }
}
