using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : MonoBehaviour
{
    public MeshRenderer myRenderer;
    public Material greenMaterial;
    public Material redMaterial;

    public bool activated = false; //true = bola esta no labirinto / false = bola esta fora do labirinto
    private bool canUse = false; //true = boy esta por baixo do helmet / false = boy nao esta debaixo do helmet

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Boy") && activated)
        {
            canUse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Boy") && activated)
        {
            canUse = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canUse)
        {
            controlBall();
        }
    }

    public void activate() {
        activated = activated ? false : true;
        myRenderer.material = activated ? greenMaterial : redMaterial;
    }

    void controlBall() { 
    }
}
