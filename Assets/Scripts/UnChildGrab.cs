using UnityEngine;
using Oculus.Interaction;

[RequireComponent(typeof(Grabbable))]
public class UnChildGrab : MonoBehaviour
{
    public GameObject objectToUnchild;

    public AudioClip useSound;

    private Grabbable grabbable;
    private bool hasUnparented = false;  // Optional: prevents repeat unparenting

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
    }

    private void OnEnable()
    {
        grabbable.WhenPointerEventRaised += HandlePointerEvent;
    }

    private void OnDisable()
    {
        grabbable.WhenPointerEventRaised -= HandlePointerEvent;
    }

    private void HandlePointerEvent(PointerEvent evt)
    {
        if (evt.Type == PointerEventType.Select && !hasUnparented)
        {
            if (objectToUnchild != null)
            {
                objectToUnchild.transform.parent = null;
                Debug.Log($"Unparented {objectToUnchild.name}");
                hasUnparented = true;

                if (useSound != null)
                    {
                        AudioSource.PlayClipAtPoint(useSound, transform.position);
                    }
            }
            else
            {
                Debug.LogWarning("No object assigned to 'objectToUnchild'.");
            }
        }
    }
}
