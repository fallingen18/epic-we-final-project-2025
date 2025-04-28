using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSplashAnimator : MonoBehaviour
{
    private SpriteRenderer sr;
    private List<Sprite> frames;
    private float frameDuration;

    public void Init(SpriteRenderer renderer, List<Sprite> sprites, float duration)
    {
        sr = renderer;
        frames = sprites;
        frameDuration = duration;

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