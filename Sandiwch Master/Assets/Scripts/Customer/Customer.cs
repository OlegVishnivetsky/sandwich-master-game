using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Sandwich sandwich;
    [SerializeField] private CustomerUI customerUI;

    [SerializeField] private Animator animator;
    [SerializeField] private List<Ingredient> sandwichRecipe;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("ROTATION TWEEN SETTINGS")]
    [SerializeField] private float duration;

    [SerializeField] private Ease ease;

    private int sandwichSize = 4;

    public bool IsCustomerInCashRegisterPosition { get; set; }

    private void Start()
    {
        sandwichSize = UnityEngine.Random.Range(3, 6);
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