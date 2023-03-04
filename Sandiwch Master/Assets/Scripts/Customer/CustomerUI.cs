using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerUI : MonoBehaviour
{
    [SerializeField] private CustomersQueue customersQueue;
    [SerializeField] private GameObject recipePanel;
    [SerializeField] private TextMeshProUGUI recipeTMPro;
    [SerializeField] private TextMeshProUGUI customerWaitingTimeTMPro;

    private List<GameObject> ingredientUIObjects = new List<GameObject>();

    private TweenScaleAnimation recipePanelScaleAnimation;
    private TweenScaleAnimation recipeTextScaleAnimation;

    private void OnEnable()
    {
        customersQueue.OnCustomerInCashRegisterPosition += EnableCustomerWaitingTimeText;
        customersQueue.OnCustomerInCashRegisterPosition += ShowCustomerRecipe;

        customersQueue.OnCustomerExitRestorant += DisableCustomerWaitingTimeText;
        customersQueue.OnCustomerExitRestorant += HideCustomerRecipe;
    }

    private void OnDisable()
    {
        customersQueue.OnCustomerInCashRegisterPosition -= EnableCustomerWaitingTimeText;
        customersQueue.OnCustomerInCashRegisterPosition -= ShowCustomerRecipe;

        customersQueue.OnCustomerExitRestorant -= DisableCustomerWaitingTimeText;
        customersQueue.OnCustomerExitRestorant -= HideCustomerRecipe;
    }

    private void Awake()
    {
        recipePanelScaleAnimation = recipePanel.GetComponent<TweenScaleAnimation>();
        recipeTextScaleAnimation = recipeTMPro.GetComponent<TweenScaleAnimation>();
    }

    private void Update()
    {
        if (customersQueue.GetFirtsCustomer() != null)
        {
            UpdateCustomerWaitingTimeText(customersQueue.GetFirtsCustomer().GetCustomerWaitingTime().ToString());
        }
    }

    private void EnableCustomerWaitingTimeText()
    {
        customerWaitingTimeTMPro.gameObject.SetActive(true);
    }

    private void DisableCustomerWaitingTimeText()
    {
        customerWaitingTimeTMPro.gameObject.SetActive(false);
    }

    public void UpdateCustomerWaitingTimeText(string text)
    {
        customerWaitingTimeTMPro.text = text;
    }

    private void ShowCustomerRecipe()
    {
        recipePanelScaleAnimation.ScaleInAnimation();
        recipeTextScaleAnimation.ScaleInAnimation();

        foreach (Ingredient ingredient in customersQueue.GetFirtsCustomer().GetSandwichRecipe())
        {
            GameObject ingredientUIObject = Instantiate(ingredient.GetIngredientUIObject(),
                new Vector3(1, 1, 1), transform.rotation, recipePanelScaleAnimation.transform);

            ingredientUIObject.transform.localPosition = new Vector3(1, 1, -150);
            ingredientUIObject.transform.localScale = Vector3.one;
            ingredientUIObjects.Add(ingredientUIObject);
        }
    }

    private void HideCustomerRecipe()
    {
        recipePanelScaleAnimation.ScaleOutAnimation();
        recipeTextScaleAnimation.ScaleOutAnimation();

        foreach (GameObject ingredient in ingredientUIObjects)
        {
            Destroy(ingredient.gameObject);
        }

        ingredientUIObjects.Clear();
    }
}