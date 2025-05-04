using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSplashAnimator : MonoBehaviour
{
    private SpriteRenderer sr;
    private List<Sprite> frames;
    private float frameDuration;
    
    // Use the current paint color from the controller
    public void Init(SpriteRenderer renderer, List<Sprite> sprites, float duration)
    {
        sr = renderer;
        frames = sprites;
        frameDuration = duration;

        // Get the controller
        PaintColorController controller = FindObjectOfType<PaintColorController>();

        // Apply the current paint color
        if (controller != null)
        {
            Color currentColor = controller.GetCurrentPaintColor();
            sr.color = currentColor;
            
            // Debug to verify color is being applied
            Debug.Log("Paint splash initialized with color: " + currentColor + 
                      " (R:" + currentColor.r + " G:" + currentColor.g + " B:" + currentColor.b + ")");
        }
        else
        {
            Debug.LogWarning("PaintSplashAnimator couldn't find PaintColorController, using white color");
            sr.color = Color.white;
        }

        // Scale the splash object down to 1/3 size
        transform.localScale *= 0.33f;

        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        for (int i = 0; i < frames.Count; i++)
        {
            sr.sprite = frames[i];
            yield return new WaitForSeconds(frameDuration);
        }

        // Hold the last frame
        sr.sprite = frames[frames.Count - 1];
        yield break;
    }
}