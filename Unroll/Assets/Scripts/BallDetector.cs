using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetector : MonoBehaviour
{
    public Helmet helmet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Equals("Ball"))
        {
            helmet.activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
