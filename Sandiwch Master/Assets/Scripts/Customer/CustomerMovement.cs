using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    [SerializeField] private Customer customer;
    [SerializeField] private CustomerAnimator customerAnimator;

    [Header("MOVEMENT")]
    [SerializeField] private float walkingSpeed;

    [Header("RAYCAST SETTINGS")]
    [SerializeField] private float raycastMaxDistance;

    [SerializeField] private LayerMask layerMask;

    public bool IsSomeoneInFront { get; private set; }

    private void Update()
    {
        bool isCanMove = !IsSomeoneInFront & !customer.IsCustomerInCashRegisterPosition;

        customerAnimator.SetBoolWalkAnimation(isCanMove);

        if (isCanMove)
            MoveCustomer();
    }

    private void FixedUpdate()
    {
        IsSomeoneInFront = IsCustomerInFront();
    }

    private void MoveCustomer()
    {
        transform.Translate(Vector3.forward * walkingSpeed * Time.deltaTime);
    }

    private bool IsCustomerInFront()
    {
        RaycastHit hit;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        if (Physics.Raycast(origin, Vector3.right, out hit, raycastMaxDistance, layerMask))
            return true;
        else
            return false;
    }
}