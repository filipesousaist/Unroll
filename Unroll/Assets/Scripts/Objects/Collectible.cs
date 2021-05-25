using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string id;
    public LoadZone load;

    private readonly float ROTATE_SPEED = 60;
/*
    private void Start()
    {
        load = FindObjectOfType<LoadZone>();
    }*/

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up, ROTATE_SPEED * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") || other.gameObject.CompareTag("Player"))
            PickUp();
    }

    private void PickUp()
    {
        // TODO: Add to collection
        //load.Collect(id);
        Destroy(gameObject);
    }
}
