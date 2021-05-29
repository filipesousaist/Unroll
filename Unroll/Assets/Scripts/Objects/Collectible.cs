using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    public enum Metal
    {
        Copper, Silver, Gold
    }
    public Metal metal;
    public LoadZone load;

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
        load.Collect(ToString());
        Destroy(gameObject);
    }

    public override string ToString()
    {
        return SceneManager.GetActiveScene().name + "-" + metal;
    }
}
