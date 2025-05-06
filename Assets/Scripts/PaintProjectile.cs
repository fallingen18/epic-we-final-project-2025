using System.Collections.Generic;
using UnityEngine;

public class PaintProjectile : MonoBehaviour
{
    public static Color ProjectilePaintColor = Color.red; // Default color

    public float speed = 20f;
    public float lifeTime = 5f;

    private List<Sprite> splashSprites;
    private float splashFrameDuration;

    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;

Transform bulletVisual = transform.Find("bullet"); // Replace with your child’s name
if (bulletVisual != null)
{
    Renderer rend = bulletVisual.GetComponent<Renderer>();
    if (rend != null)
    {
        rend.material.color = ProjectilePaintColor;
    }
}
else
{
    Debug.LogWarning("BulletModel child not found on PaintProjectile.");
}

        Destroy(gameObject, lifeTime);
    }

    public void Setup(List<Sprite> sprites, float frameDuration)
    {
        splashSprites = sprites;
        splashFrameDuration = frameDuration;
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 hitPos = contact.point;
        Quaternion rot = Quaternion.LookRotation(contact.normal);

        GameObject splashObj = new GameObject("PaintSplash");

        var collider = splashObj.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = new Vector3(1f, 1f, 0.1f);

        splashObj.transform.position = hitPos + contact.normal * 0.01f;
        splashObj.transform.rotation = rot;

        splashObj.AddComponent<PaintSplash>();

        var sr = splashObj.AddComponent<SpriteRenderer>();
        sr.color = ProjectilePaintColor; // Use the static color

        splashObj.AddComponent<PaintSplashAnimator>().Init(sr, splashSprites, splashFrameDuration);

        Destroy(gameObject);
    }
}