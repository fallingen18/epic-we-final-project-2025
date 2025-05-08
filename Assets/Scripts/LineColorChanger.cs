using UnityEngine;

public class LineColorManager : MonoBehaviour
{
    // Public material that you can assign in the Unity Inspector
    public Material lineMaterial;

    // Public static variable to hold the current color of the line
    public static Color lineColor = Color.red; // Default color is red
    void awake(){
        if (lineMaterial != null)
        {
            lineMaterial.color = lineColor;
        }
    }
    void Start()
    {
        // If a material is assigned, set its color to the static lineColor at the start
        if (lineMaterial != null)
        {
            lineMaterial.color = lineColor;
        }
    }

    // Method to set the color of the material
    public static void SetLineColor(Color newColor)
    {
        lineColor = newColor;

        // Find all instances of this script in the scene and update their materials
        LineColorManager[] allLineManagers = FindObjectsOfType<LineColorManager>();
        foreach (var manager in allLineManagers)
        {
            if (manager.lineMaterial != null)
            {
                manager.lineMaterial.color = lineColor;
            }
        }
    }

    // Optionally, you can update the color every frame, depending on your needs
    void Update()
    {
        if (lineMaterial != null && lineMaterial.color != lineColor)
        {
            lineMaterial.color = lineColor;
        }
    }
}
