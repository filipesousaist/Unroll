using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public ElementalColor color;

    private Collider myCollider;

    private BlocksManager blocksManager;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
        blocksManager = FindObjectOfType<BlocksManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.gameObject.GetComponent<Ball>();
        if (ball != null && ball.color.Equals(color))
        {
            blocksManager.RemoveBlock(this);
            Destroy(gameObject);
        }    
    }

    public void SetBreakable(bool breakable)
    {
        myCollider.isTrigger = breakable;
    }
}
