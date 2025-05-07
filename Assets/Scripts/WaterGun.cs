using UnityEngine;
using System.Collections;
 
public class WaterGun : MonoBehaviour
{
    public GameObject waterParticlePrefab;
    public Transform muzzlePoint;
    public GameObject sprayColliderObject; // Assign a child GameObject with trigger collider
    public float fadeDuration = 1f;
     
    public TwoHandGrabDetector grabDetector;
    private bool isSecondHandGrabbing = false;

    public Transform leftHandTransform;
    public Transform rightHandTransform;
    public float proximityThreshold = 0.25f;
 
    private GameObject activeParticles;

    public AudioSource useSound;

    private bool isPlayingSound = false;

    void Start()
    {
        // Initialize the spray collider - make sure it has the WaterSpray tag
        if (sprayColliderObject != null)
        {
            sprayColliderObject.tag = "WaterSpray";
            
            // Make sure it has a collider
            Collider collider = sprayColliderObject.GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = true;
                collider.enabled = false;
            }
            else
            {
                Debug.LogError("Spray collider object needs a Collider component");
            }
            
            // Add the WaterSprayCollider component if it doesn't have it
            if (sprayColliderObject.GetComponent<WaterSprayCollider>() == null)
            {
                sprayColliderObject.AddComponent<WaterSprayCollider>();
            }
        }
        else
        {
            Debug.LogError("Spray collider object not assigned in the inspector");
        }

        if (grabDetector != null)
        {
            grabDetector.OnSecondHandGrabbed += HandleSecondHandGrab;
            grabDetector.OnSecondHandReleased += HandleSecondHandRelease;
        }
    }
 
void Update()
{
    if (isSecondHandGrabbing && IsSprayButtonPressed())
    {
        // Start spraying water
        StartSpraying();

        if (!isPlayingSound)
        {
            useSound.Play();
            isPlayingSound = true;
        }
    }
    else
    {
        // Stop spraying water
        StopSpraying();

        if (isPlayingSound)
        {
            useSound.Stop();
            isPlayingSound = false;
        }
    }
}

    private void StartSpraying()
    {
        if (activeParticles == null)
        {
            activeParticles = Instantiate(waterParticlePrefab, muzzlePoint.position, muzzlePoint.rotation, muzzlePoint);
        }

        if (sprayColliderObject != null)
        {
            Collider collider = sprayColliderObject.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
            
            // Make sure the spray particles are positioned correctly
            sprayColliderObject.transform.position = muzzlePoint.position;
            sprayColliderObject.transform.rotation = muzzlePoint.rotation;
        }
    }

    private void StopSpraying()
    {
        if (activeParticles != null)
        {
            Destroy(activeParticles);
            activeParticles = null;
        }

        if (sprayColliderObject != null)
        {
            Collider collider = sprayColliderObject.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    private void HandleSecondHandGrab()
    {
        isSecondHandGrabbing = true;
        Debug.Log("Second hand grab detected");
    }

    private void HandleSecondHandRelease()
    {
        isSecondHandGrabbing = false;
        Debug.Log("Second hand released");
    }
 
    private bool IsSprayButtonPressed()
    {
        bool rightTrigger = OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
        bool leftTrigger = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        bool aPressed = OVRInput.Get(OVRInput.Button.One);
        bool xPressed = OVRInput.Get(OVRInput.Button.Three);
 
        return rightTrigger || leftTrigger || aPressed || xPressed;
    }
}