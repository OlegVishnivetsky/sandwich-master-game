using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float duration;
    [SerializeField] private bool isTimerUseSlider;
    private float currentValue;

    public UnityEvent OnTimerEnd;

    private void OnEnable()
    {
        OnTimerEnd.AddListener(() => GameManager.Instance.SwitchGameStateTo(GameState.DayEnded));
    }

    private void Start()
    {
        currentValue = duration;

        if (isTimerUseSlider)
        {
            slider.maxValue = duration;
        }
    }

    private void Update()
    {
        if (isTimerUseSlider)
        {
            currentValue -= Time.deltaTime;
            slider.value = currentValue;
        }
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