using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private PickUpItem keyVariable; // Reference to the key tracking script
    public Transform door; // Assign the door GameObject in the Inspector
    public float openAngle = 90f; // The angle to open the door
    public float openSpeed = 2f; // Speed of door opening

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpening = false;

    void Start()
    {
        keyVariable = GameObject.Find("Player").GetComponent<PickUpItem>();
        
        if (door == null) 
            door = transform; // If no door assigned, assume this script is on the door

        closedRotation = door.rotation;
        openRotation = door.rotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        if (keyVariable.collectedKeys == 8) 
        {
            isOpening = true;
        }

        if (isOpening)
        {
            door.rotation = Quaternion.Slerp(door.rotation, openRotation, Time.deltaTime * openSpeed);
        }
    }
}
