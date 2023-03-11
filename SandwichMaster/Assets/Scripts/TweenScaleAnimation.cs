using System;
using UnityEngine;

public class TweenScaleAnimation : MonoBehaviour
{
    [Header("TWEENING SCALE PARAMETERS")]
    [SerializeField, Range(0, 1f)] private float scaleInDuration = 0.5f;
    [SerializeField, Range(0, 1f)] private float scaleOutDuration = 0.5f;

    [Header("TWEENING CLICK PARAMETERS")]
    [SerializeField, Range(0, 1f)] private float scaleClickDuration = 0.5f;

    [SerializeField] private LeanTweenType ease;

    [SerializeField] private bool playOnAwakeScaleInAnimation;

    [SerializeField] private Vector3 originalScale;

    public void Awake()
    {
        if (playOnAwakeScaleInAnimation)
            ScaleInAnimation(gameObject, ease);
    }

    private void Start()
    {
        if (originalScale == Vector3.zero)
            originalScale = transform.localScale;
    }

    public float GetScaleInDuration()
    {
        return scaleInDuration;
    }

    public float GetScaleOutDuration()
    {
        return scaleOutDuration;
    }

    public void ScaleInAnimation(GameObject gameObject)
    {
        ScaleInAnimation(gameObject, ease);
    }

    public void ScaleInAnimation(GameObject gameObject, LeanTweenType ease)
    {
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, originalScale, scaleInDuration).setEase(ease);
    }

    public void ScaleOutAnimation(GameObject gameObject)
    {
        LeanTween.scale(gameObject, Vector3.zero, scaleOutDuration).setEase(ease);
    }

    public void ScaleOutAnimation(GameObject gameObject, LeanTweenType ease, float duration, Action onCompleteAction)
    {
        LeanTween.scale(gameObject, Vector3.zero, duration).setEase(ease).setOnComplete(onCompleteAction);
    }

    public void ScaleClickAnimation()
    {
        LeanTween.scale(gameObject, transform.localScale * 1.2f, scaleClickDuration).setEase(LeanTweenType.punch);
    }
}