using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float duration;
    private float currentValue;

    public UnityEvent OnTimerEnd;

    public event Action<float> OnTimerCurrentValueChanged;

    private bool IsTimerActive = true;

    private void Start()
    {
        currentValue = duration;
    }

    public float GetTimerDuration() 
    { 
        return currentValue; 
    }

    public float GetTimerCurrentValue()
    {
        return currentValue;
    }

    public void TimerTick(float deltaTime)
    {
        if (!IsTimerActive)
            return;

        currentValue -= deltaTime;
        OnTimerCurrentValueChanged?.Invoke(currentValue);

        if (currentValue <= 0)
        {
            IsTimerActive = false;
            OnTimerEnd?.Invoke();
        }
    }
}