using System;
using System.Collections;
using UnityEngine;

public class CustomersSpawner : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Customer customerPrefab;
    [SerializeField] private Transform spawnPoint;

    public event Action<Customer> OnCustomerSpawned;

    [Header("SPAWN SETTINGS")]
    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;

    private float spawnInterval;

    private void OnEnable()
    {
        GameManager.OnDayStartedState += StartCustomersSpawn;
    }

    private void OnDisable()
    {
        GameManager.OnDayStartedState -= StartCustomersSpawn;
    }

    public void SetSpawnDelay(float min, float max)
    {
        minSpawnDelay = min;
        maxSpawnDelay = max;
    }

    private void StartCustomersSpawn()
    {
        StartCoroutine(StartCustomersSpawnRoutine());
    }

    public IEnumerator StartCustomersSpawnRoutine()
    {
        while (GameManager.Instance.GetCurrentGameState() == GameState.DayStarted)
        {
            spawnInterval = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay);

            yield return new WaitForSeconds(spawnInterval);

            Customer spawnedCustomer = Instantiate(customerPrefab, spawnPoint.position,
                customerPrefab.transform.rotation);
            OnCustomerSpawned?.Invoke(spawnedCustomer);
        }
    }
}