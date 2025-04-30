using UnityEngine;

public class PaintSplash : MonoBehaviour
{
    // Tag for the water gun's spray collider
    public string waterSprayTag = "WaterSpray";

    void OnTriggerEnter(Collider other)
    {
        // Check if this paint splash was hit by a water spray
        if (other.CompareTag(waterSprayTag))
        {
            Debug.Log("Paint splash hit by water spray - destroying");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Also check for physical collisions with water spray objects
        if (collision.gameObject.CompareTag(waterSprayTag))
        {
            Debug.Log("Paint splash collided with water spray - destroying");
            Destroy(gameObject);
        }
    }
}