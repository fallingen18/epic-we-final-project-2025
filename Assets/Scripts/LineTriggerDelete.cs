using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LineTriggerDelete : MonoBehaviour
{

    public string waterSprayTag = "WaterSpray";

   void OnTriggerEnter(Collider other)
    {
        // Optionally check what 'other' is before deleting (e.g., if it's a specific tag)
          Debug.Log("line go brrrrrr outside");
        // Destroys the parent GameObject of the collider
        if (transform.parent != null && other.CompareTag(waterSprayTag))
        {
            Debug.Log("line go brrrrrr trigger");
            Destroy(transform.parent.gameObject);

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Also check for physical collisions with water spray objects
        if (transform.parent != null && collision.gameObject.CompareTag(waterSprayTag))
        {
            Debug.Log("line go brrrrrr collision");
            Destroy(transform.parent.gameObject);
        }
    }
}
