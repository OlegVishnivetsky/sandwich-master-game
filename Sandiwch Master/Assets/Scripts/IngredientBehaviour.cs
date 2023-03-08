using UnityEngine;

public enum IngredientType
{
    BreadSlice,
    Other
}

public class IngredientBehaviour : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Ingredient ingredientPrefab;

    [SerializeField] private CustomButton customButton;
    [SerializeField] private Sandwich sandwich;

    public void Start()
    {
        customButton.OnPointerClickEvent.AddListener(SelectIngredient);
        GameManager.Instance.AddToAvailableIngredientsList(ingredientPrefab);
    }

    public void SelectIngredient()
    {
        sandwich.ChooseAndPlaceSandwichIngredient(ingredientPrefab);
    }
}