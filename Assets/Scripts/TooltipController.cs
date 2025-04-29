using UnityEngine;

public class TooltipController : MonoBehaviour
{
    public Transform player;          
    public GameObject tooltip;        
    public float showDistance = 5f;  
    public float hideDistance = 0.5f;
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Show or hide tooltip based on distance
        tooltip.SetActive(distance <= showDistance && distance >= hideDistance);

        // Make tooltip face the player
        if (tooltip.activeSelf)
        {
            tooltip.transform.LookAt(player);
            tooltip.transform.rotation = Quaternion.LookRotation(tooltip.transform.position - player.position); // Flip if needed
        }
    }
}