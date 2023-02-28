using System.Collections.Generic;
using UnityEngine;

public class CustomerUI : MonoBehaviour
{
    [SerializeField] private CustomersQueue customersQueue;
    [SerializeField] private GameObject recipePanel;

    private List<GameObject> ingredientUIObjects = new List<GameObject>();

    private TweenScaleAnimation recipePanelScaleAnimation;

    private void OnEnable()
    {
        customersQueue.OnCustomerInCashRegisterPosition += ShowCustomerRecipe;
        customersQueue.OnCustomerExitRestorant += HideCustomerRecipe;
    }

    private void OnDisable()
    {
        customersQueue.OnCustomerInCashRegisterPosition -= ShowCustomerRecipe;
        customersQueue.OnCustomerExitRestorant -= HideCustomerRecipe;
    }

    private void Awake()
    {
        recipePanelScaleAnimation = recipePanel.GetComponent<TweenScaleAnimation>();
    }

    private void ShowCustomerRecipe()
    {
        recipePanelScaleAnimation.ScaleInAnimation();

        foreach (Ingredient ingredient in customersQueue.GetFirtsCustomer().GetSandwichRecipe())
        {
            GameObject ingredientUIObject = Instantiate(ingredient.GetIngredientUIObject(),
                new Vector3(1, 1, 1), transform.rotation, recipePanel.transform);

            ingredientUIObject.transform.localPosition = new Vector3(1, 1, -150);
            ingredientUIObject.transform.localScale = Vector3.one;
            ingredientUIObjects.Add(ingredientUIObject);
        }
    }

    private void HideCustomerRecipe()
    {
        recipePanelScaleAnimation.ScaleOutAnimation();

        foreach (GameObject ingredient in ingredientUIObjects)
        {
            Destroy(ingredient.gameObject);
        }

        ingredientUIObjects.Clear();
    }
}