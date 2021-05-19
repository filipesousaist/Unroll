using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetector : MonoBehaviour
{
    public Helmet helmet;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Equals("Ball")  && !helmet.activated)
        {
            helmet.activate();
        }
    }
}
