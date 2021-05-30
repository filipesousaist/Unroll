using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalReaction : MonoBehaviour
{
    public ElementalColor reactionColor;
    public ElementalColor neutralColor;
    public Material cubeMaterial;

    public Vector3 boyInitPos;
    public Vector3 ballInitPos;

    private bool tp = false;
    private Ball ball;
    private Boy boy;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        boy = FindObjectOfType<Boy>();
    }

    private void Update()
    {
        if (tp)
        {
            tp = false;

            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            boy.transform.position = boyInitPos;
            boy.HasDied();
            ball.transform.position = ballInitPos;
            ball.ChangeColor(neutralColor);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball") && ball.color.Equals(reactionColor))
        {
            GameObject cubePath = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubePath.transform.position = collision.transform.position + Vector3.down * 0.95f;
            cubePath.GetComponent<MeshRenderer>().material = cubeMaterial;
        }
        else
            tp = true;
    }
}
