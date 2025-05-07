using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundWhenMoved : MonoBehaviour
{
    public float movementThreshold = 0.01f; // Adjust for sensitivity
    private AudioSource moveSound;
    private Vector3 lastPosition;

    void Start()
    {
        moveSound = GetComponent<AudioSource>();
        moveSound.loop = true;
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float distanceMoved = (currentPosition - lastPosition).magnitude;

        if (distanceMoved > movementThreshold)
        {
            if (!moveSound.isPlaying)
                moveSound.Play();
        }
        else
        {
            if (moveSound.isPlaying)
                moveSound.Pause();
        }

        lastPosition = currentPosition;
    }
}
