using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Sandwich sandwich;

    [SerializeField] private Animator animator;
    [SerializeField] private List<Ingredient> sandwichRecipe;

    [Header("MOVEMENT")]
    [SerializeField] private float walkingSpeed;

    [Header("RAYCAST SETTINGS")]
    [SerializeField] private float raycastMaxDistance;

    [SerializeField] private LayerMask layerMask;

    [Header("ROTATION TWEEN SETTINGS")]
    [SerializeField] private float duration;

    [SerializeField] private Ease ease;

    public bool IsSomeoneInFront { get; private set; }
    public bool IsCustomerInCashRegisterPosition { get; set; }

    private int sandwichSize = 4;

    private void Start()
    {
        sandwichSize = UnityEngine.Random.Range(3, 6);
    }

    private void Update()
    {
        bool isCanMove = !IsSomeoneInFront & !IsCustomerInCashRegisterPosition;

        animator.SetBool("IsWalking", isCanMove);

        if (isCanMove)
            MoveCustomer();
    }

    private void FixedUpdate()
    {
        IsSomeoneInFront = IsCustomerInFront();
    }

    public List<Ingredient> GetSandwichRecipe()
    {
        return sandwichRecipe;
    }

    private void MoveCustomer()
    {
        transform.Translate(Vector3.forward * walkingSpeed * Time.deltaTime);
    }

    public void ExitRestorant()
    {
        RotateCustomerTween(new Vector3(0, 90, 0), () =>
        {
            IsCustomerInCashRegisterPosition = false;
            Destroy(gameObject, 4f);
        });
    }

    public void RotateCustomerTween(Vector3 endValue)
    {
        transform.DORotate(endValue, duration).SetEase(ease);
    }

    public void RotateCustomerTween(Vector3 endValue, TweenCallback OnCompleteRotation)
    {
        transform.DORotate(endValue, duration).SetEase(ease).OnComplete(OnCompleteRotation);
    }

    public void GenerateSandwichRecipe()
    {
        if (sandwichRecipe.Count == 0)
            sandwichRecipe.Add(GameManager.Instance.GetAvailableIngredientsByType(IngredientType.BreadSlice));

        for (int i = 0; i < sandwichSize - 1; i++)
            sandwichRecipe.Add(GameManager.Instance.GetRandomAvailableIngredient());

        sandwichRecipe.Add(GameManager.Instance.GetAvailableIngredientsByType(IngredientType.BreadSlice));
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