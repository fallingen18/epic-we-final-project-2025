using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public float baseJumpForce = 10f; // Base jump force for the trampoline
    public float jumpForceMultiplier = 1.5f; // Multiplier to increase jump force
    public float maxJumpForce = 50f; // Max jump force the trampoline can apply
    public float resetTime = 2f; // Time after which the boost resets

    private float currentJumpForce; // Current jump force for the player
    private float lastTimeHit; // Time when the trampoline was last hit by the player

    void Start()
    {
        currentJumpForce = baseJumpForce; // Start with base jump force
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.gameObject == player)
        {
            // Get the player's PlayerController
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // Debugging: Check if we are hitting the trampoline correctly
                Debug.Log("Player hit trampoline!");

                // Only apply the force if the player is falling or on the ground
                if (playerController.IsGrounded() || playerController.GetVelocity().y <= 0)
                {
                    // Apply the trampoline force: Modify the y-velocity directly
                    Vector3 newVelocity = playerController.GetVelocity();
                    newVelocity.y = currentJumpForce;
                    playerController.SetVelocity(newVelocity);

                    // Increase the jump force for the next hit, but cap it
                    currentJumpForce = Mathf.Min(currentJumpForce * jumpForceMultiplier, maxJumpForce);

                    // Update the last time the trampoline was hit
                    lastTimeHit = Time.time;

                    // Optional: Debugging line
                    Debug.Log("Current Jump Force: " + currentJumpForce);
                }
            }
        }
    }

    void Update()
    {
        // If the trampoline hasn't been hit for a while, reset the jump force
        if (Time.time - lastTimeHit > resetTime)
        {
            currentJumpForce = baseJumpForce; // Reset to base jump force
        }
    }
}
