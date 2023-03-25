using UnityEngine;

public class BigSandwichTime : MonoBehaviour
{
    [SerializeField] private CustomersSpawner customersSpawner;
    [SerializeField] private int bigSandwichSize;

    private void OnEnable()
    {
        customersSpawner.OnCustomerSpawned += StartBigSandwichTime;
    }

    private void OnDisable()
    {
        customersSpawner.OnCustomerSpawned -= StartBigSandwichTime;
    }

    private void StartBigSandwichTime(Customer customer)
    {
        int randomNumber = Random.Range(0, 10);

        if (randomNumber == 6)
        {
            CreateBigSandwichCustomer(customer);
        }
    }

    private void CreateBigSandwichCustomer(Customer customer)
    {
        customer.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        customer.SetSandwichRecpeSize(bigSandwichSize);
        customer.GetComponent<Timer>().SetTimerDuration(20f);
    }
}