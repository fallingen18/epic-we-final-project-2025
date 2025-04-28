using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 4f;
    
    public float groundLevel = -2.95f; // Ground level
    public float colliderHeight = 3f;  // Height of the player collider
    public float groundCheckThreshold = 0.3f; // Distance from ground to consider grounded

    private float rotationX = 0f;
    private Vector3 velocity;
    private CharacterController controller;
    private bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Ground Check (based on camera position and height of the player)
        isGrounded = CheckGrounded();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Jump
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Move the character (including gravity)
        controller.Move(velocity * Time.deltaTime);
    }

    bool CheckGrounded()
    {
        // Use camera's y position and subtract half the collider's height to check bottom of the character
        float playerHeight = Camera.main.transform.position.y - colliderHeight / 2f;
        return Mathf.Abs(playerHeight - groundLevel) < groundCheckThreshold;
    }

    // Getter for isGrounded
    public bool IsGrounded()
    {
        return isGrounded;
    }

    // Getter for velocity
    public Vector3 GetVelocity()
    {
        return velocity;
    }

    // Setter for velocity
    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
    }
}
