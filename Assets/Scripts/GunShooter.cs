using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour
{
    public Transform muzzlePoint;
    public GameObject paintProjectilePrefab;

    public GameObject tshirtprojectilePrefab;
        public List<Sprite> splashSprites;
    public float splashFrameDuration = 0.05f;

    //public Transform leftHandTransform;   // Assign in inspector or get from OVRHand
    //public Transform rightHandTransform;  // Assign in inspector or get from OVRHand
    public TwoHandGrabDetector grabDetector;

    public float proximityThreshold = 0.25f; // How close the hand must be to "hold" gun
    private bool isSecondHandGrabbing = false;


     private void Start()
    {
        if (grabDetector != null)
        {
            grabDetector.OnSecondHandGrabbed += HandleSecondHandGrab;
             grabDetector.OnSecondHandReleased += HandleSecondHandRelease;
        }
    }

    private void Update()
    {
        if (isSecondHandGrabbing == true && IsShootButtonPressed())
        {
             ShootPaint();
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

    //bool IsHoldingGun()
    //{
    //    bool isCloseToLeftHand = Vector3.Distance(leftHandTransform.position, transform.position) < proximityThreshold;
    //    bool isCloseToRightHand = Vector3.Distance(rightHandTransform.position, transform.position) < proximityThreshold;
//
    //    bool isLeftGripping = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger);    // Left grip
    //    bool isRightGripping = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger); // Right grip
//
    //    return isCloseToLeftHand && isCloseToRightHand && isLeftGripping && isRightGripping;
    //}

    bool IsShootButtonPressed()
    {
        bool aPressed = OVRInput.GetDown(OVRInput.Button.One);
        bool xPressed = OVRInput.GetDown(OVRInput.Button.Three);
        bool rightTrigger = OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger);
        bool leftTrigger = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);

        return aPressed || xPressed || rightTrigger || leftTrigger;
    }

    void ShootPaint()
    {
        if(paintProjectilePrefab != null){
            GameObject proj = Instantiate(
            paintProjectilePrefab,
            muzzlePoint.position,
            muzzlePoint.rotation
        );
        proj.GetComponent<PaintProjectile>().Setup(splashSprites, splashFrameDuration);
        }
else{
    GameObject proj = Instantiate(
            tshirtprojectilePrefab,
            muzzlePoint.position,
            muzzlePoint.rotation
        );
}
        
    }
}