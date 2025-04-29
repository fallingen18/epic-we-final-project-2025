using UnityEngine;
using System.Collections;
 
public class WaterGun : MonoBehaviour
{
    public GameObject waterParticlePrefab;
    public Transform muzzlePoint;
    public Collider sprayCollider; // Assign a trigger collider (IsTrigger = true)
    public float fadeDuration = 1f;
     
     public TwoHandGrabDetector grabDetector;
     private bool isSecondHandGrabbing = false;

    public Transform leftHandTransform;
    public Transform rightHandTransform;
    public float proximityThreshold = 0.25f;
 
    public float sprayCheckRadius = 0.3f;  // radius for manual overlap check
    public LayerMask splashLayerMask;      // layer mask for PaintSplash objects
 
    private GameObject activeParticles;
    private readonly Collider[] overlapResults = new Collider[10]; // reusable array for efficiency
 
    void Start()
    {
        if (sprayCollider != null)
        {
            sprayCollider.enabled = false;
        }

        if (grabDetector != null)
        {
            grabDetector.OnSecondHandGrabbed += HandleSecondHandGrab;
             grabDetector.OnSecondHandReleased += HandleSecondHandRelease;
        }
    }
 
    void Update()
    {
        if (isSecondHandGrabbing == true && IsSprayButtonPressed())
        {
            if (activeParticles == null)
            {
                activeParticles = Instantiate(waterParticlePrefab, muzzlePoint.position, muzzlePoint.rotation, muzzlePoint);
            }
 
            if (sprayCollider != null)
                sprayCollider.enabled = true;
 
            // Actively check for collisions while spraying
            CheckSprayCollisions();
        }
        else
        {
            if (activeParticles != null)
            {
                Destroy(activeParticles);
                activeParticles = null;
            }
 
            if (sprayCollider != null)
                sprayCollider.enabled = false;
        }
    }

     private void HandleSecondHandGrab()
    {
        // Do something when the second hand grabss
        isSecondHandGrabbing = true;
        
        Debug.Log("grab worked");
    }

    private void HandleSecondHandRelease()
{
    isSecondHandGrabbing = false;
    Debug.Log("release worked");
}
 
    private void CheckSprayCollisions()
    {
        int hits = Physics.OverlapSphereNonAlloc(muzzlePoint.position, sprayCheckRadius, overlapResults, splashLayerMask);
        for (int i = 0; i < hits; i++)
        {
            Collider other = overlapResults[i];
            if (other != null && other.name.StartsWith("PaintSplash"))
            {
                StartCoroutine(FadeAndDestroy(other.gameObject));
            }
        }
    }
 
    //private bool IsHoldingGun()
    //{
    //    bool isCloseToLeftHand = Vector3.Distance(leftHandTransform.position, transform.position) < proximityThreshold;
    //    bool isCloseToRightHand = Vector3.Distance(rightHandTransform.position, transform.position) < proximityThreshold;
 //
    //    bool isLeftGripping = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger);
    //    bool isRightGripping = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);
 //
    //    return isCloseToLeftHand && isCloseToRightHand && isLeftGripping && isRightGripping;
    //}
 
    private bool IsSprayButtonPressed()
    {
        bool rightTrigger = OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
        bool leftTrigger = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        bool aPressed = OVRInput.Get(OVRInput.Button.One);
        bool xPressed = OVRInput.Get(OVRInput.Button.Three);
 
        return rightTrigger || leftTrigger || aPressed || xPressed;
    }
 
    private IEnumerator FadeAndDestroy(GameObject splash)
    {
        Renderer rend = splash.GetComponent<Renderer>();
        if (rend == null) yield break;
 //
        //Color startColor = rend.material.color;
        //float elapsed = 0f;
 //
        //while (elapsed < fadeDuration)
        //{
        //    float t = elapsed / fadeDuration;
        //    rend.material.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 0f, t));
        //    elapsed += Time.deltaTime;
        //    yield return null;
        //}
 
        Destroy(splash);
    }
}