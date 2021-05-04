using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private readonly float ROTATE_SPEED = 60;

    public ElementalColor color;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, ROTATE_SPEED * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Ball ball;
        if (other.gameObject.name.Equals("ECM_Ball"))
        {
            ball = other.gameObject.GetComponentInChildren<Ball>();
        }
        else
        {
            ball = other.gameObject.GetComponent<Ball>();
        }
        if (ball != null)
        {
            ball.ChangeColor(color);
        }
    }
}
