using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 teleportLocation;
    public GameObject teleportTarget; // Drag the exact GameObject here in the Inspector

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            //Debug.LogError("No CharacterController found on Player!");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("Collided with: " + hit.gameObject.name);

        if (hit.gameObject == teleportTarget)
        {
            //Debug.Log("Teleport target matched. Teleporting to: " + teleportLocation);
            controller.enabled = false;
            transform.position = teleportLocation;
            controller.enabled = true;
        }
        else
        {
            //Debug.Log("Hit object is not the teleport target.");
        }
    }
}
