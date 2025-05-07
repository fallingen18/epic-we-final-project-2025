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
                string currentTexName = rend.material.mainTexture.name;

                if (currentTexName.StartsWith("tsa_"))
                {
                    // Currently wearing TSA texture — switch back
                    string originalTexName = currentTexName.Substring(4); // Remove "tsa_"
                    Texture2D originalTex = Resources.Load<Texture2D>(originalTexName);

                    if (originalTex != null)
                    {
                        rend.material.mainTexture = originalTex;
                    }
                    else
                    {
                        Debug.LogWarning($"Original texture '{originalTexName}' not found in Resources.");
                    }
                }
                else
                {
                    // Currently wearing original — switch to TSA version
                    string tsaTexName = "tsa_" + currentTexName;
                    Texture2D tsaTex = Resources.Load<Texture2D>(tsaTexName);

                    if (tsaTex != null)
                    {
                        rend.material.mainTexture = tsaTex;
                    }
                    else
                    {
                        Debug.LogWarning($"TSA texture '{tsaTexName}' not found in Resources.");
                    }
                }
            }
        }

        Destroy(gameObject);
    }
}
