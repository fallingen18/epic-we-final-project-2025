using UnityEngine;

public class WaterSprayCollider : MonoBehaviour
{
    // This script should be attached to your water spray collider object
    // No behavior needed - just make sure this object has the "WaterSpray" tag
    
    void Start()
    {
        // Verify this object has the correct tag
        if (!gameObject.CompareTag("WaterSpray"))
        {
            //Debug.LogWarning("WaterSprayCollider object should have the 'WaterSpray' tag");
        }
    }
}