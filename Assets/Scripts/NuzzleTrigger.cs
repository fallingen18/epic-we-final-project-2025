using UnityEngine;

public class NuzzleTrigger : MonoBehaviour
{
    // Reference to the PaintSplash's material to fade out
    public Renderer paintSplashRenderer;
    private bool isFading = false;
    private float fadeDuration = 2f;  // Time to fully fade out
    private float fadeTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("PaintSplash"))
        {
            // Start fading out the PaintSplash
            isFading = true;
            fadeTime = 0f;

            // Make the object invisible by fading
            paintSplashRenderer = other.GetComponent<Renderer>();
        }
    }

    private void Update()
    {
        if (isFading && paintSplashRenderer != null)
        {
            // Gradually reduce the alpha of the PaintSplash material
            fadeTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeTime / fadeDuration);
            Color currentColor = paintSplashRenderer.material.color;
            currentColor.a = alpha;
            paintSplashRenderer.material.color = currentColor;

            // Once it is fully faded, you can deactivate or destroy the object
            if (alpha <= 0f)
            {
                isFading = false;
                // Optionally deactivate or destroy the object
                paintSplashRenderer.gameObject.SetActive(false);
                // or
                // Destroy(paintSplashRenderer.gameObject);
            }
        }
    }
}
