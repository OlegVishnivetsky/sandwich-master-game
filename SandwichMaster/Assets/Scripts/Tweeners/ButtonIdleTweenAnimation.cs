using UnityEngine;

public class ButtonIdleTweenAnimation : MonoBehaviour
{
    [SerializeField] private float tweenDuration;

    private void Start()
    {
        AnimateButtonIdle();
    }

    private void AnimateButtonIdle()
    {
         LeanTween.scale(gameObject, transform.localScale * 1.2f, tweenDuration)
            .setEase(LeanTweenType.punch).setLoopClamp();
    }
}