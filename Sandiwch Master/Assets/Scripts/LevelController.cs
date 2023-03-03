using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int goalAmountOfServedCustomers;
    private int currentAmountOfServedCustomers;

    [Header("COMPONENTS")]
    [SerializeField] private CustomersQueue customersQueue;
    [SerializeField] private Slider levelSlider;

    private void OnEnable()
    {
        customersQueue.OnCustomerExitRestorant += IncreaseCurrentAmountOfServedCustomers;
    }

    private void OnDisable()
    {
        customersQueue.OnCustomerExitRestorant -= IncreaseCurrentAmountOfServedCustomers;
    }

    private void Start()
    {
        levelSlider.maxValue = goalAmountOfServedCustomers;
        currentAmountOfServedCustomers = 0;

        UpdateLevelSliderValue();
    }

    private void IncreaseCurrentAmountOfServedCustomers()
    {
        if (currentAmountOfServedCustomers >= goalAmountOfServedCustomers) return;

        currentAmountOfServedCustomers++;
        UpdateLevelSliderValue();
    }

    private void UpdateLevelSliderValue()
    {
        levelSlider.value = currentAmountOfServedCustomers;
        
        if (currentAmountOfServedCustomers == goalAmountOfServedCustomers)
        {
            GameManager.Instance.SwitchGameStateTo(GameState.DayEnded);
        }
    }
}