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
        GameManager.OnDayStartedState += SpawnCustomer;
        GameManager.OnDayStartedState += StartCustomersSpawn;
    }

    private void OnDisable()
    {
        GameManager.OnDayStartedState -= SpawnCustomer;
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
        while (GameManager.Instance.GetCurrentGameState() != GameState.DayEnded)
        {
            spawnInterval = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay);

            yield return new WaitForSeconds(spawnInterval);
            SpawnCustomer();
        }
    }

    private void SpawnCustomer()
    {
        Customer spawnedCustomer = Instantiate(customerPrefab, spawnPoint.position,
                        customerPrefab.transform.rotation);
        spawnedCustomer.SetCustomerRandomColor();
        spawnedCustomer.SetSandwichRecpeSize(UnityEngine.Random.Range(3,
            Mathf.CeilToInt(GameManager.Instance.GetNumberOfClientsServed() * 0.5f + 3)));

        OnCustomerSpawned?.Invoke(spawnedCustomer);
    }
}