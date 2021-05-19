using UnityEngine;

public class Collectible : MonoBehaviour
{
    private readonly float ROTATE_SPEED = 60;

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
        Destroy(gameObject);
    }
}
