using DG.Tweening;
using UnityEngine;

public class TweenScaleAnimation : MonoBehaviour
{
    [Header("TWEENING SCALE PARAMETERS")]
    [SerializeField, Range(0, 1f)] private float scaleInDuration = 0.5f;
    [SerializeField, Range(0, 1f)] private float scaleOutDuration = 0.5f;

    [Header("TWEENING CLICK PARAMETERS")]
    [SerializeField, Range(0, 1f)] private float scaleClickDuration = 0.5f;

    [SerializeField] private Ease ease;

    [SerializeField] private bool playOnAwakeScaleInAnimation;

    [SerializeField] private Vector3 originalScale;

    public void Awake()
    {
        if (playOnAwakeScaleInAnimation)
            ScaleInAnimation(ease);
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

    public void ScaleInAnimation()
    {
        ScaleInAnimation(ease);
    }

    public void ScaleInAnimation(Ease ease)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(originalScale, scaleInDuration).SetEase(ease);
    }

    public void ScaleOutAnimation()
    {
        transform.DOScale(Vector3.zero, scaleInDuration).SetEase(ease);
    }

    public void ScaleOutAnimation(Ease ease, float duration, TweenCallback tweenCallback)
    {
        transform.DOScale(Vector3.zero, duration).SetEase(ease).OnComplete(tweenCallback);
    }

    public void ScaleClickAnimation()
    {
        transform.DOScale(transform.localScale * 1.2f, scaleClickDuration).SetEase(ease).SetLoops(2, LoopType.Yoyo);
    }
}