using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : MonoBehaviour
{
    public GameObject useHelmetTextBox;

    public MeshRenderer myRenderer;
    public Material greenMaterial;
    public Material redMaterial;

    public Transform boy;

    public Ball ball;

    public Camera boyCamera;

    public bool activated = false; //true = bola esta no labirinto / false = bola esta fora do labirinto
    private bool canUse = false; //true = boy esta por baixo do helmet / false = boy nao esta debaixo do helmet
    private bool ballMode = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Boy") && activated)
        {
            useHelmetTextBox.SetActive(true);
            canUse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Boy"))
        {
            useHelmetTextBox.SetActive(false);
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
        activated = true;
        myRenderer.material = greenMaterial;
    }

    public void deactivate() {
        activated = false;
        myRenderer.material = redMaterial;
    }

    public void controlBall() {
        if (!ballMode)
        {
            boy.gameObject.SetActive(false);
            boyCamera.gameObject.SetActive(false);
            ball.ActivateControl();
            ballMode = true;
        }
        else
        {
            boy.gameObject.SetActive(true);
            boyCamera.gameObject.SetActive(true);
            ball.DeactivateControl();
            ballMode = false;
        }
    }
}
