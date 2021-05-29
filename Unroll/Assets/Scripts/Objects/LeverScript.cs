using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public Transform plank;
    public Boy boy;
    bool canInteract = false;

    private void OnTriggerEnter(Collider other)
    {
        canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canInteract || boy.HasBall())
        {
            canInteract = false;
        }

        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                plank.Rotate(0, 0, 10);
            }
        }
    }
}
