using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float baseBounceForce = 15f; // Good VR starting value
    public float maxBounceForce = 35f;    // Comfortable VR maximum
    public float forceIncrease = 1.2f;    // Multiplier per bounce
    public float cooldownTime = 2f;       // Time until force resets
    
    [Header("Effects")]
    public AudioSource bounceSound;
    public ParticleSystem bounceParticles;
    
    private float currentBounceForce;
    private float lastBounceTime;
    private OVRPlayerController playerController;
    private CharacterController characterController;

    void Start()
    {
        currentBounceForce = baseBounceForce;
        playerController = FindObjectOfType<OVRPlayerController>();
        characterController = playerController.GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Only bounce if hitting the player's CharacterController
        if (other == characterController)
        {
            // Only bounce when coming downward
            if (characterController.velocity.y <= 0)
            {
                ApplyBounce();
            }
        }
    }

    void ApplyBounce()
    {
        // Disable OVR controller temporarily
        playerController.enabled = false;
        
        // Calculate bounce velocity (upward with current force)
        Vector3 bounceVelocity = characterController.velocity;
        bounceVelocity.y = currentBounceForce;
        
        // Apply the bounce
        characterController.Move(bounceVelocity * Time.deltaTime);
        
        // Re-enable controller
        playerController.enabled = true;
        
        // Play effects
        if (bounceSound) bounceSound.Play();
        if (bounceParticles) bounceParticles.Play();
        
        // Increase force for next bounce (capped at max)
        currentBounceForce = Mathf.Min(currentBounceForce * forceIncrease, maxBounceForce);
        lastBounceTime = Time.time;
        
        // Schedule force reset
        Invoke(nameof(ResetBounceForce), cooldownTime);
    }

    void ResetBounceForce()
    {
        currentBounceForce = baseBounceForce;
    }
}