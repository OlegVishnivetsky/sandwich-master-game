using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Sandwich sandwich;
    [SerializeField] private CustomerUI customerUI;

    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField] private List<Ingredient> sandwichRecipe;

    [Space(5)]
    [SerializeField] private float customerWaitingTime;

    [Header("ROTATION TWEEN SETTINGS")]
    [SerializeField] private float duration;

    [SerializeField] private Ease ease;

    private int sandwichSize = 4;

    public bool IsCustomerWaitingTimeUp;
    public bool IsCustomerInCashRegisterPosition { get; set; }

    private void Start()
    {
        sandwichSize = Random.Range(3, 6);
    }

    private void Update()
    {
        TimerCustomerWaitingTime();
    }

    public List<Ingredient> GetSandwichRecipe()
    {
        return sandwichRecipe;
    }

    public int GetCustomerWaitingTime()
    {
        return (int)customerWaitingTime;
    }

    public void SetCustomerWaitingTime(float value)
    {
        customerWaitingTime = value;
    }

    public void SetCustomerRandomColor()
    {
        skinnedMeshRenderer.material.color = new Color32((byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
    }

    private void TimerCustomerWaitingTime()
    {
        if (IsCustomerInCashRegisterPosition && customerWaitingTime >= 0)
        {
            customerWaitingTime -= 1 * Time.deltaTime;

            if (customerWaitingTime <= 0)
                IsCustomerWaitingTimeUp = true;
        }
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
}