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
    
        // Add collider FIRST (before other components)
        var collider = splashObj.AddComponent<BoxCollider>();
        collider.isTrigger = true; // Make it a trigger for better detection
        collider.size = new Vector3(1f, 1f, 0.1f); // Flat collider for decals
        
        // Position adjustment (prevents z-fighting)
        splashObj.transform.position = hitPos + contact.normal * 0.01f; 
        splashObj.transform.rotation = Quaternion.LookRotation(contact.normal);
        
        // Add the PaintSplash component to handle its own destruction
        splashObj.AddComponent<PaintSplash>();
        
        // Visual components
        var sr = splashObj.AddComponent<SpriteRenderer>();
        splashObj.AddComponent<PaintSplashAnimator>().Init(sr, splashSprites, splashFrameDuration);

        Destroy(gameObject);
    }
}