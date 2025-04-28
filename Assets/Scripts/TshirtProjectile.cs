using UnityEngine;

public class TShirtProjectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;

    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime); // Auto-destroy after lifetime
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hitObj = collision.gameObject;

        if (hitObj.name.StartsWith("tsa_"))
        {
            Renderer rend = hitObj.GetComponent<Renderer>();
            if (rend != null && rend.material != null && rend.material.mainTexture != null)
            {
                string originalTexName = rend.material.mainTexture.name;

                string newTexName = "tsa_" + originalTexName;
                Texture2D newTex = Resources.Load<Texture2D>(newTexName);

                if (newTex != null)
                {
                    rend.material.mainTexture = newTex;
                }
                else
                {
                    Debug.LogWarning($"Texture '{newTexName}' not found in Resources.");
                }
            }
        }

        Destroy(gameObject);
    }
}
