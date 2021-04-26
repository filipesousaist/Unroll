using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public ElementalColor color;

    private void MyOnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null && ball.color.Equals(color))
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.gameObject.GetComponent<Ball>();
        if (ball != null && ball.color.Equals(color))
            Destroy(gameObject);
    }
}
