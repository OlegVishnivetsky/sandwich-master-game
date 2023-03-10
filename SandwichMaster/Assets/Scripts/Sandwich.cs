using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandwich : MonoBehaviour
{
    [SerializeField] private CustomersQueue customersQueue;
    [SerializeField] private Transform spawnSandwichPoint;
    [SerializeField] private float verticalOffset;
    [SerializeField] private LeanTweenType ease;

    private List<Ingredient> sandwichIngredients = new List<Ingredient>();
    private List<Ingredient> ingredientObjects = new List<Ingredient>();

    private Ingredient lastIngredient;

    public event Action OnSandwichIsDone;

    private float ingredientVerticalOffset;

    private float amountOfMistakes;

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
                ingredientVerticalOffset + ingredient.GetVerticalOffset(),
                spawnSandwichPoint.position.z));
        }
    }

    private void PlaceSandwichIngredient(Ingredient ingredient, Vector3 position)
    { 
        sandwichIngredients.Add(ingredient);

        Ingredient ingredientObject = Instantiate(ingredient,
            position, Quaternion.identity);
        ingredientVerticalOffset = ingredientObject.transform.position.y;

        ingredientObjects.Add(ingredientObject);

        CheckFirstCustomerSandwichCorrectness();
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
            GameManager.Instance.IsIngredientButtonsInteractable = false;
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
            ingredienTweenScaleAnimation.ScaleOutAnimation(ingredientObjects[i].gameObject);

            Destroy(ingredientObjects[i].gameObject, 5f);
        }

        ClearIngredientLists();
    }

    private void RemoveLastIngredient()
    {
        if (sandwichIngredients.Count - 1 == 0) return;

        Ingredient ingredientToRemove = sandwichIngredients[sandwichIngredients.Count - 1];
        Ingredient ingredientObjectToRemove = ingredientObjects[ingredientObjects.Count - 1];

        ingredientObjectToRemove.GetComponent<TweenScaleAnimation>().ScaleOutAnimation(gameObject, ease, 0.2f, () =>
        {
            sandwichIngredients.Remove(ingredientToRemove);
            ingredientObjects.Remove(ingredientObjectToRemove);

            lastIngredient = ingredientObjects[ingredientObjects.Count - 1];

            Destroy(ingredientToRemove.gameObject);
        });
    }

    private void ClearIngredientLists()
    {
        sandwichIngredients.Clear();
        ingredientObjects.Clear();
    }
}