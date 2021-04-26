using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private MeshRenderer myRenderer;

    public ElementalColor color;


    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeColor(ElementalColor newColor)
    {
        color = newColor;
        myRenderer.material = newColor.ballMaterial;
    }
}
