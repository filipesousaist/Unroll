using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public MeshCollider col;
    public MeshRenderer myRenderer;
    public Material material;
    public Transform mazeDoor;
    private float translationConst = 1.42f;
    // Start is called before the first frame update
    void Start()
    { 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Ball")) {
            myRenderer.material = material;
            mazeDoor.position = new Vector3(mazeDoor.position.x - translationConst, mazeDoor.position.y, mazeDoor.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
