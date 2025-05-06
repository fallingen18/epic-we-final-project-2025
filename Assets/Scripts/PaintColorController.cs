using UnityEngine;

// Attach this to your Paint_Cannon GameObject
public class PaintColorController : MonoBehaviour
{
    // Current paint color - this will be used by the splash animation
    [SerializeField] private Color currentPaintColor = Color.red; // Start with red so we can see if it's working

    // Singleton instance
    private static PaintColorController _instance;
    public static PaintColorController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("PaintColorController instance not found!");
            }
            return _instance;
        }
    }

    private Renderer objectRenderer;

    private void Awake()
    {
        // Set up the singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogWarning("No Renderer found on this GameObject!");
        }
        else
        {
            // Set the initial material color
            objectRenderer.material.color = currentPaintColor;
        }

        Debug.Log("PaintColorController initialized with color: " + currentPaintColor);
    }

    // Method to get the current paint color
    public Color GetCurrentPaintColor()
    {
        Debug.Log("Getting paint color: " + currentPaintColor);
        return currentPaintColor;
    }

    // Method to set the current paint color
    public void SetPaintColor(Color newColor)
    {
        Debug.Log("Setting paint color from " + currentPaintColor + " to " + newColor);
        currentPaintColor = newColor;

        if (objectRenderer != null)
        {
            objectRenderer.material.color = newColor;
        }
    }
}
