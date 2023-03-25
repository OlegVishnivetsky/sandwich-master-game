using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomersQueue : MonoBehaviour
{
    private Queue<Customer> customersQueue = new Queue<Customer>();

    [Header("COMPONENTS")]
    [SerializeField] private Sandwich sandwich;

    [SerializeField] private Transform cashRegisterTransform;
    [SerializeField] private CustomersSpawner customersSpawner;

    private Customer firstCustomerInQueue;

    public event Action OnCustomerInCashRegisterPosition;

    public event Action OnCustomerExitRestorant;

    private void OnEnable()
    {
        GameManager.OnDayEndedState += ServeAllCustomers;
        customersSpawner.OnCustomerSpawned += AddCustomersToQueue;
        sandwich.OnSandwichIsDone += ServeClient;
    }

    private void OnDisable()
    {
        GameManager.OnDayEndedState -= ServeAllCustomers;
        customersSpawner.OnCustomerSpawned -= AddCustomersToQueue;
        sandwich.OnSandwichIsDone -= ServeClient;
    }

    private void Update()
    {
        if (customersQueue.Count > 0)
        {
            if (firstCustomerInQueue.IsCustomerTimerEnd)
            {
                ServeClient();
            }
        }

        if (customersQueue.Count > 0 && GameManager.Instance.GetCurrentGameState() == GameState.DayIsGoing)
        {
            if (!customersQueue.Peek().IsCustomerInCashRegisterPosition)
            {
                GameManager.Instance.IsIngredientButtonsInteractable = false;
                CheckFirstCustomerPosition();
            }
        }
    }

    private void ServeAllCustomers()
    {
        for (int i = 0; i < customersQueue.Count; i++)
        {
            cashRegisterTransform.gameObject.SetActive(false);
            customersQueue.Dequeue().IsCustomerInCashRegisterPosition = false;
            ServeClient();
        }
    }

    private void ServeClient()
    {
        OnCustomerExitRestorant?.Invoke();
        firstCustomerInQueue.ExitRestorant();

        RemoveFirtCustomerInQueue();
        GameManager.Instance.IncreaseNumberOfClientsServed();
    }

    public Customer GetFirtsCustomer()
    {
        if (customersQueue.Count > 0)
            return customersQueue.Peek();

        return null;
    }

    private void CheckFirstCustomerPosition()
    {
        if (firstCustomerInQueue.transform.position.x >= cashRegisterTransform.position.x 
            && GameManager.Instance.GetCurrentGameState() != GameState.DayEnded)
        {
            GameManager.Instance.IsIngredientButtonsInteractable = true;

            firstCustomerInQueue.IsCustomerInCashRegisterPosition = true;
            firstCustomerInQueue.RotateCustomerTween(new Vector3(0, 180, 0));
            firstCustomerInQueue.GenerateSandwichRecipe();

            OnCustomerInCashRegisterPosition?.Invoke();
        }
    }

    public void AddCustomersToQueue(Customer customer)
    {
        customersQueue.Enqueue(customer);

        if (customersQueue.Count == 1)
        {
            firstCustomerInQueue = customersQueue.Peek();
        }
    }

    public void RemoveFirtCustomerInQueue()
    {

        if (customersQueue.Count > 0)
        {
            customersQueue.Dequeue();
            firstCustomerInQueue = customersQueue.Peek();
        }
    }
}