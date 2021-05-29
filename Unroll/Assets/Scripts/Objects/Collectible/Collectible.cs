﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    public enum Metal
    {
        Copper, Silver, Gold
    }
    public Metal metal;

    private LoadZone load;

    private readonly float ROTATE_SPEED = 60;

    private void Awake()
    {
        load = FindObjectOfType<LoadZone>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up, ROTATE_SPEED * Time.deltaTime);
    }

    public void PickUp()
    {
        // TODO: Add to collection
        load.Collect(ToString());
        Destroy(gameObject);
    }

    public override string ToString()
    {
        return SceneManager.GetActiveScene().name + "-" + metal;
    }

    public static string GetLevelName(string id)
    {
        return id.Substring(0, id.LastIndexOf('-'));
    }

    public static string GetMetalName(string id)
    {
        return id.Substring(id.LastIndexOf('-') + 1);
    }
}
