using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalReaction : MonoBehaviour
{

    public ElementalColor reactionColor;
    public Material cubeMaterial;

    private Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            if (ball.color.Equals(reactionColor))
            {
                GameObject cubePath = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubePath.transform.position = collision.transform.position - Vector3.up*0.95f;
                cubePath.GetComponent<MeshRenderer>().material = cubeMaterial;
            }
            else
            {
                Debug.Log("Game Over");
            }
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
}
