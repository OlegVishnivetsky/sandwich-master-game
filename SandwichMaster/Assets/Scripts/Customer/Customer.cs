using System.Collections.Generic;
using UnityEngine;
using System;

public class Customer : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Sandwich sandwich;

    [SerializeField] private CustomerUI customerUI;

    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField] private List<Ingredient> sandwichRecipe;

    [Space(5)]
    [SerializeField] private Timer customerTimer;

    [Header("ROTATION TWEEN SETTINGS")]
    [SerializeField] private float duration;

    [SerializeField] private LeanTweenType ease;

    private int sandwichSize = 4;

    public bool IsCustomerTimerEnd;
    public bool IsCustomerInCashRegisterPosition { get; set; }

    private void OnEnable()
    {
        customerTimer.OnTimerEnd.AddListener(() => IsCustomerTimerEnd = true);
    }

    private void Start()
    {
        sandwichSize = UnityEngine.Random.Range(3, 6);
    }

    private void Update()
    {
        if (IsCustomerInCashRegisterPosition)
        {
            customerTimer.TimerTick(Time.deltaTime);
        }
    }

    public List<Ingredient> GetSandwichRecipe()
    {
        return sandwichRecipe;
    }

    public void SetCustomerRandomColor()
    {
        skinnedMeshRenderer.material.color = new Color32((byte)UnityEngine.Random.Range(0, 255),
            (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
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
        LeanTween.rotate(gameObject, endValue, duration).setEase(ease);
    }

    public void RotateCustomerTween(Vector3 endValue, Action OnCompleteRotation)
    {
        LeanTween.rotate(gameObject, endValue, duration).setEase(ease).setOnComplete(OnCompleteRotation);
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