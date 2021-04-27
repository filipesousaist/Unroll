using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ElementalColor color;

    private MeshRenderer myRenderer;

    private BlocksManager blocksManager;

    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        blocksManager = FindObjectOfType<BlocksManager>();
    }

    public void ChangeColor(ElementalColor newColor)
    {
        blocksManager.ChangeBreakableBlocks(color, newColor);
        myRenderer.material = newColor.ballMaterial;
        color = newColor;
    }
}
