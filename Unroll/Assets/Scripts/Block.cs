using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public ElementalColor color;

    private Collider myCollider;

    private BlocksManager blocksManager;

    private Ball ball;

    private Rigidbody ballRigidbody;

    private readonly float BALL_SPEED_DECREASE = 5;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
        blocksManager = FindObjectOfType<BlocksManager>();
        ball = FindObjectOfType<Ball>();
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && ball.color.Equals(color))
        {
            blocksManager.RemoveBlock(this);
            Destroy(transform.parent.gameObject);

            if (ballRigidbody.velocity.magnitude < BALL_SPEED_DECREASE)
                ballRigidbody.velocity = Vector3.zero;
            else
                ballRigidbody.velocity -= ballRigidbody.velocity.normalized * BALL_SPEED_DECREASE;
        }    
    }

    public void SetBreakable(bool breakable)
    {
        transform.parent.gameObject.layer = LayerMask.NameToLayer(
            breakable ? "Breakable Blocks" : "Blocks");
    }
}
