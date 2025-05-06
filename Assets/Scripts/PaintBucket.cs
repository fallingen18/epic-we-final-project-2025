using UnityEngine;

public class PaintBucket : MonoBehaviour
{
    [Header("Color Settings")]
    [Tooltip("The color this paint bucket will apply")]
    public Color bucketColor = Color.red;

    [Header("Effects")]
    [Tooltip("Visual effect to spawn when used")]
    public GameObject useEffect;
    [Tooltip("Sound to play when used")]
    public AudioClip useSound;

    [Header("Settings")]
    [Tooltip("Should the bucket be destroyed after use?")]
    public bool destroyAfterUse = true;

    private void OnCollisionEnter(Collision collision)
    {
        CheckForPaintCannon(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckForPaintCannon(other.gameObject);
    }

    private void CheckForPaintCannon(GameObject obj)
{
    if (obj.CompareTag("PaintCannon") || obj.name.Contains("Paint_Cannon"))
    {
        PaintColorController paintController = obj.GetComponentInParent<PaintColorController>();

        if (paintController != null)
        {
            // Set paint color globally
            paintController.SetPaintColor(bucketColor);

            // Set projectile color directly
            PaintProjectile.ProjectilePaintColor = bucketColor;

            if (useEffect != null)
            {
                Instantiate(useEffect, transform.position, Quaternion.identity);
            }

            if (useSound != null)
            {
                AudioSource.PlayClipAtPoint(useSound, transform.position);
            }

            if (destroyAfterUse)
            {
                Destroy(gameObject);
            }
        }
    }
}

}