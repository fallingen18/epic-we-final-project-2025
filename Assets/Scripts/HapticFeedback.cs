using UnityEngine;
using UnityEngine.XR;

public class HapticFeedback : MonoBehaviour
{
    public static HapticFeedback Instance { get; private set; }

    public enum ControllerHand
    {
        Left,
        Right,
        Both
    }

    void Awake()
    {
        // Enforce Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerHaptic(ControllerHand hand, float amplitude, float duration)
    {
        if (hand == ControllerHand.Left || hand == ControllerHand.Both)
            SendHapticTo(XRNode.LeftHand, amplitude, duration);

        if (hand == ControllerHand.Right || hand == ControllerHand.Both)
            SendHapticTo(XRNode.RightHand, amplitude, duration);
    }

    private void SendHapticTo(XRNode node, float amplitude, float duration)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);
        if (device.isValid)
        {
            if (device.TryGetHapticCapabilities(out HapticCapabilities capabilities) && capabilities.supportsImpulse)
            {
                uint channel = 0; // Usually channel 0
                device.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }
}
