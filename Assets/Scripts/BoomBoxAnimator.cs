using UnityEngine;

public class BoomBoxAnimator : MonoBehaviour
{
    public AudioClip musicClip;
    public Transform[] speakerElements; // Assign Left and Right speaker GameObjects in Inspector
    public float pulseSpeed = 5f;
    public float pulseAmount = 0.05f;
    public float bodyPulseAmount = 0.03f;

    private AudioSource audioSource;
    private Vector3 originalScale;
    private Vector3[] speakerOriginalScales;

    void Start()
    {
        // Setup AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.spatialBlend = 1.0f; // 3D sound
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 20f;
        audioSource.Play();

        // Save original scales
        originalScale = transform.localScale;
        speakerOriginalScales = new Vector3[speakerElements.Length];
        for (int i = 0; i < speakerElements.Length; i++)
        {
            speakerOriginalScales[i] = speakerElements[i].localScale;
        }
    }

    void Update()
    {
        float pulse = Mathf.Sin(Time.time * pulseSpeed);

        // Animate main boombox body
        float bodyScale = 1f + bodyPulseAmount * pulse;
        transform.localScale = originalScale * bodyScale;

        // Animate speakers
        for (int i = 0; i < speakerElements.Length; i++)
        {
            float speakerScale = 1f + pulseAmount * pulse;
            speakerElements[i].localScale = speakerOriginalScales[i] * speakerScale;
        }
    }
}
