using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerUI : MonoBehaviour
{
    [SerializeField] private CustomersQueue customersQueue;
    [SerializeField] private GameObject recipePanel;
    [SerializeField] private GameObject recipeHeaderImage;
    [SerializeField] private TextMeshProUGUI customerWaitingTimeTMPro;

    private List<GameObject> ingredientUIObjects = new List<GameObject>();

    private TweenScaleAnimation recipePanelScaleAnimation;
    private TweenScaleAnimation recipeHeaderImageScaleAnimation;

    private Timer firstCustomerTimer;

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
        recipeHeaderImageScaleAnimation = recipeHeaderImage.GetComponent<TweenScaleAnimation>();
    }

    private void Update()
    {
        if (firstCustomerTimer != null)
        {
            UpdateCustomerWaitingTimeText(firstCustomerTimer.GetTimerCurrentValue().ToString("#.0"));
        }
    }

    private void EnableCustomerWaitingTimeText()
    {
        firstCustomerTimer = customersQueue.GetFirtsCustomer().GetComponent<Timer>();
        customerWaitingTimeTMPro.gameObject.SetActive(true);
    }

    private void DisableCustomerWaitingTimeText()
    {
        firstCustomerTimer = null;
        customerWaitingTimeTMPro.gameObject.SetActive(false);
    }

    public void UpdateCustomerWaitingTimeText(string text)
    {
        customerWaitingTimeTMPro.text = text;
    }

    private void ShowCustomerRecipe()
    {
        recipePanelScaleAnimation.ScaleInAnimation();
        recipeHeaderImageScaleAnimation.ScaleInAnimation();

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
        recipeHeaderImageScaleAnimation.ScaleOutAnimation();

        foreach (GameObject ingredient in ingredientUIObjects)
        {
            Destroy(ingredient.gameObject);
        }

        ingredientUIObjects.Clear();
    }
}