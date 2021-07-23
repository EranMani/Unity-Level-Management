using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFader : ScreenFader
{
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private float _delay = 0.3f;

    protected void Awake()
    {
        // Clamp the life time of the transition by the sum of all actions related to fading
        _lifeTime = Mathf.Clamp(_lifeTime, FadeOnDuration + FadeOffDuration + _delay, 10f);
    }

    private IEnumerator PlayRoutine()
    {
        SetAlpha(_clearAlpha);
        yield return new WaitForSeconds(_delay);

        FadeOn();
        // Wait for the graphics to be fully opaque
        float onTime = _lifeTime - (FadeOffDuration + _delay);
        yield return new WaitForSeconds(onTime);

        // Fade off and destroy transition
        Fadeoff();
        Object.Destroy(gameObject, FadeOffDuration);
    }

    public void PlayTransition()
    {
        StartCoroutine(PlayRoutine());
    }

    // This transition will happen occasionally and will need to be accessible from anywhere, to play transition from one scene to another
    public static void PlayTransition(TransitionFader transitionPrefab)
    {
        if (transitionPrefab != null)
        {
            TransitionFader instance = Instantiate(transitionPrefab, Vector3.zero, Quaternion.identity);
            instance.PlayTransition();
        }
    }
}
