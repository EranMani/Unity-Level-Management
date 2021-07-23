using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private float _solidAlpha = 1f;
    [SerializeField] private float _clearAlpha = 0f;
    [SerializeField] private float _fadeDuration = 2f;

    // MaskableGraphic: a base class for all the UI elements that will be faded
    [SerializeField] private MaskableGraphic[] graphicsToFade;

    private void SetAlpha(float alpha)
    {
        foreach (MaskableGraphic graphic in graphicsToFade)
        {
            if (graphic != null)
            {
                graphic.canvasRenderer.SetAlpha(alpha);
            }
        }
    }

    private void Fade(float targetAlpha, float duration)
    {
        foreach (MaskableGraphic graphic in graphicsToFade)
        {
            if (graphic != null)
            {
                // Tweens the alpha of the CanvasRenderer color associated with this Graphic
                graphic.CrossFadeAlpha(targetAlpha, duration, true);
            }
        }
    }

    public void Fadeoff()
    {
        SetAlpha(_solidAlpha);
        Fade(_clearAlpha, _fadeDuration);
    }

    public void FadeOn()
    {
        SetAlpha(_clearAlpha);
        Fade(_solidAlpha, _fadeDuration);
    }
}
