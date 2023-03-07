using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Timer timer;
    [SerializeField] private Slider levelSlider;

    private void OnEnable()
    {
        timer.OnTimerCurrentValueChanged += UpdateLevelSliderValue;
        timer.OnTimerEnd.AddListener(() => GameManager.Instance.SwitchGameStateTo(GameState.DayEnded));
    }

    private void OnDisable()
    {
        timer.OnTimerCurrentValueChanged -= UpdateLevelSliderValue;
    }

    private void Start()
    {
        levelSlider.maxValue = timer.GetTimerDuration();
    }

    private void Update()
    {
        timer.TimerTick(Time.deltaTime);
    }

    private void UpdateLevelSliderValue(float value)
    {
        levelSlider.value = value;
    }
}