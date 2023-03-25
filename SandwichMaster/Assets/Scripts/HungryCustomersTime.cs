using System;
using System.Collections;
using UnityEngine;

public class HungryCustomersTime : MonoBehaviour
{
    [SerializeField] private CustomersSpawner customerSpawner;
    [SerializeField] private float hungryCustomersTimeDuration;

    public event Action OnHungryCustomersTimeStarted;

    private void OnEnable()
    {
        customerSpawner.OnCustomerSpawned += StartHungryCustomersTime;
    }

    private void OnDisable()
    {
        customerSpawner.OnCustomerSpawned -= StartHungryCustomersTime;
    }

    private void StartHungryCustomersTime(Customer custromer)
    {
        int randomNumber = UnityEngine.Random.Range(0, 10);

        if (randomNumber == 5)
        {
            StartCoroutine(StartHungryCustomersTimeRoutine());
        }
    }

    public IEnumerator StartHungryCustomersTimeRoutine()
    {
        OnHungryCustomersTimeStarted?.Invoke();

        customerSpawner.SetSpawnDelay(2f, 3f);
        yield return new WaitForSeconds(hungryCustomersTimeDuration);
        customerSpawner.SetSpawnDelay(8f, 15f);
    }
}