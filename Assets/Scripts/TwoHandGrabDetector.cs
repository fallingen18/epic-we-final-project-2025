using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;  // Make sure you include this for grabbing functionality

public class TwoHandGrabDetector : MonoBehaviour
{
    // List to keep track of hands that are grabbing the object
    private List<int> grabbers = new List<int>();

    // Event for when the second hand grabs the object
    public event System.Action OnSecondHandGrabbed;
    public event System.Action OnSecondHandReleased;
    private int previousGrabberCount = 0;


    // This is your own custom logic for when grab events occur
    private void OnEnable()
    {
        // Subscribe to PointerEvent raised from the Grabbable (or another source)
        var grabbable = GetComponent<Grabbable>();
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised += HandlePointerEvent;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        var grabbable = GetComponent<Grabbable>();
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised -= HandlePointerEvent;
        }
    }

    // Handle pointer events that track grabs
    private void HandlePointerEvent(PointerEvent evt)
    {
        switch (evt.Type)
        {
            case PointerEventType.Select:
                // A hand just selected/grabbed the object
                if (!grabbers.Contains(evt.Identifier))
                {
                    grabbers.Add(evt.Identifier);
                }
                CheckForSecondHandGrab();
                break;

            case PointerEventType.Unselect:
                // A hand just released the object
                grabbers.Remove(evt.Identifier);
                Debug.Log("released!");
                OnSecondHandReleased?.Invoke();
    

    previousGrabberCount = grabbers.Count;
    break;
        }
    }

    // Check if we just moved from 1 hand to 2 hands grabbing
    private void CheckForSecondHandGrab()
    {
        if (grabbers.Count == 2)
        {
            // Only trigger when second hand grabs the object
            OnSecondHandGrabbed?.Invoke();
        }
    }
}
