using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandwich : MonoBehaviour
{
    [SerializeField] private CustomersQueue customersQueue;
    [SerializeField] private Transform spawnSandwichPoint;
    [SerializeField] private float verticalOffset;
    [SerializeField] private Ease ease;

    private List<Ingredient> sandwichIngredients = new List<Ingredient>();
    private List<Ingredient> ingredientObjects = new List<Ingredient>();

    private Transform lastIngredientTransform;

    public event Action OnSandwichIsDone;

    private int amountOfMistakes;

    private void OnEnable()
    {
        customersQueue.OnCustomerExitRestorant += RemoveAllIngredientObjects;
    }

    private void OnDisable()
    {
        customersQueue.OnCustomerExitRestorant -= RemoveAllIngredientObjects;
    }

    public void ChooseAndPlaceSandwichIngredient(Ingredient ingredient)
    {
        if (sandwichIngredients.Count == 0)
        {
            if (ingredient.GetIngredientType() != IngredientType.BreadSlice) return;

            PlaceSandwichIngredient(ingredient, spawnSandwichPoint.position);
        }
        else
        {
            PlaceSandwichIngredient(ingredient, new Vector3(spawnSandwichPoint.position.x,
                lastIngredientTransform.position.y + verticalOffset,
                spawnSandwichPoint.position.z));
        }
    }

    private void PlaceSandwichIngredient(Ingredient ingredient, Vector3 position)
    {
        sandwichIngredients.Add(ingredient);

        Ingredient ingredientObject = Instantiate(ingredient,
            position, Quaternion.identity);

        ingredientObjects.Add(ingredientObject);

        CheckFirstCustomerSandwichCorrectness();

        if (sandwichIngredients.Count > 0)
        {
            lastIngredientTransform = ingredientObjects[ingredientObjects.Count - 1].gameObject.transform;
        }
    }

    private void CheckFirstCustomerSandwichCorrectness()
    {
        if (customersQueue.GetFirtsCustomer() != null)
        {
            CheckSandwichCorrectness(customersQueue.GetFirtsCustomer().GetSandwichRecipe());
        }
    }

    private void CheckSandwichCorrectness(List<Ingredient> sandwichRecipe)
    {
        if (IsSandwichDone())
        {
            OnSandwichIsDone?.Invoke();

            CompareSandwichWithRecipe(sandwichRecipe);
            RemoveAllIngredientObjects();
        }

        bool IsSandwichDone()
        {
            return (sandwichIngredients.Count > 1 && sandwichIngredients[0].GetIngredientType() == IngredientType.BreadSlice
                && sandwichIngredients[sandwichIngredients.Count - 1].GetIngredientType() == IngredientType.BreadSlice);
        }
    }

    private void CompareSandwichWithRecipe(List<Ingredient> sandwichRecipe)
    {
        amountOfMistakes = 0;

        for (int i = 0; i < sandwichIngredients.Count; i++)
        {
            if (sandwichRecipe.Count > i)
            {
                if (sandwichIngredients[i] == sandwichRecipe[i])
                    continue;
            }

            amountOfMistakes++;
        }
    }

    private void RemoveAllIngredientObjects()
    {
        StartCoroutine(RemoveAllIngredientObjectsRoutine());
    }

    private IEnumerator RemoveAllIngredientObjectsRoutine()
    {
        yield return new WaitForSeconds(0.1f);

        if (ingredientObjects.Count == 0)
            yield return null;

        for (int i = 0; i < ingredientObjects.Count; i++)
        {
            TweenScaleAnimation ingredienTweenScaleAnimation = ingredientObjects[i].GetComponent<TweenScaleAnimation>();
            ingredienTweenScaleAnimation.ScaleOutAnimation();

            Destroy(ingredientObjects[i].gameObject, ingredienTweenScaleAnimation.GetScaleOutDuration());
        }

        ClearIngredientLists();
    }

    private void RemoveLastIngredient()
    {
        if (sandwichIngredients.Count - 1 == 0) return;

        Ingredient ingredientToRemove = sandwichIngredients[sandwichIngredients.Count - 1];
        Ingredient ingredientObjectToRemove = ingredientObjects[ingredientObjects.Count - 1];

        ingredientObjectToRemove.GetComponent<TweenScaleAnimation>().ScaleOutAnimation(ease, 0.2f, () =>
        {
            sandwichIngredients.Remove(ingredientToRemove);
            ingredientObjects.Remove(ingredientObjectToRemove);

            lastIngredientTransform = ingredientObjects[ingredientObjects.Count - 1].gameObject.transform;

            Destroy(ingredientObjectToRemove.gameObject);
        });
    }

    private void ClearIngredientLists()
    {
        sandwichIngredients.Clear();
        ingredientObjects.Clear();
    }
}