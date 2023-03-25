using UnityEngine;

public class DayEndedPanel : MonoBehaviour
{
    [SerializeField] private TweenScaleAnimation tweenScaleAnimation;

    private void OnEnable()
    {
        GameManager.OnDayEndedState += ShowDayEndedPanel;
    }

    private void OnDisable()
    {
        GameManager.OnDayEndedState -= ShowDayEndedPanel;
    }

    private void ShowDayEndedPanel()
    {
        tweenScaleAnimation.ScaleInAnimation(gameObject);
    }
}