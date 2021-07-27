using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] protected float _solidAlpha = 1f;
    [SerializeField] protected float _clearAlpha = 0f;
    [SerializeField] private float _fadeOnDuration = 2f;
    [SerializeField] private float _fadeOffDuration = 2f;

    public float FadeOnDuration { get { return _fadeOnDuration; } }
    public float FadeOffDuration { get { return _fadeOffDuration; } }

    // MaskableGraphic: a base class for all the UI elements that will be faded
    [SerializeField] private MaskableGraphic[] graphicsToFade;

    protected void SetAlpha(float alpha)
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
        Fade(_clearAlpha, _fadeOffDuration);
    }

    public void FadeOn()
    {
        SetAlpha(_clearAlpha);
        Fade(_solidAlpha, _fadeOnDuration);
    }
}
