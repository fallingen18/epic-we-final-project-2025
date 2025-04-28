using System.Collections.Generic;
using UnityEngine;
 
public class PaintProjectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;
 
    private List<Sprite> splashSprites;
    private float splashFrameDuration;
 
    public void Setup(List<Sprite> sprites, float frameDuration)
    {
        splashSprites = sprites;
        splashFrameDuration = frameDuration;
    }
 
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }
 
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 hitPos = contact.point;
        Quaternion rot = Quaternion.LookRotation(contact.normal);
 
        GameObject splashObj = new GameObject("PaintSplash");
        splashObj.transform.position = hitPos + contact.normal * 0.01f;
        splashObj.transform.rotation = rot;
 
        // Add SpriteRenderer and animation component
        var sr = splashObj.AddComponent<SpriteRenderer>();
        splashObj.AddComponent<PaintSplashAnimator>().Init(sr, splashSprites, splashFrameDuration);
 
        // Add a small collider so WaterGun can detect it
        var collider = splashObj.AddComponent<BoxCollider>();
        collider.isTrigger = false; // important!
        collider.size = new Vector3(5f, 5f, 0.01f); // Adjust to match your splash size
 
        Destroy(gameObject); // Destroy the projectile
    }
}