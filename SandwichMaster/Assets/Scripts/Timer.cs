using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float duration;
    [SerializeField] private bool isTimerUseSlider;
    private bool isTimerActive = false;

    private float currentValue;

    public UnityEvent OnTimerEnd;

    private void OnEnable()
    {
        GameManager.OnDayStartedState += ActivateTimer;
    }

    private void OnDisable()
    {
        GameManager.OnDayStartedState -= ActivateTimer;
    }

    private void Start()
    {
        isTimerActive = false;
        currentValue = duration;

        if (isTimerUseSlider)
        {
            slider.maxValue = duration;
        }
    }

    private void Update()
    {
        if (isTimerUseSlider && isTimerActive)
        {
            currentValue -= Time.deltaTime;
            slider.value = currentValue;

            if (currentValue <= 0)
            {
                GameManager.Instance.EndTheDay();
                isTimerActive = false;
            }
        }
    }

    public void SetTimerDuration(float duration)
    {
        this.duration = duration;
    }

    private void ActivateTimer()
    {
        isTimerActive = true;
    }

    public float GetTimerCurrentValue()
    {
        return currentValue;
    }

    public void TimerTick(float deltaTime)
    {
        currentValue -= deltaTime;

        if (currentValue <= 0)
        {
            OnTimerEnd?.Invoke();
        }
    }
}