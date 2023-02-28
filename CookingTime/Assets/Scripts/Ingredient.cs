using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private GameObject ingredientUIObject;
    [SerializeField] private IngredientType ingredientType;

    public GameObject GetIngredientUIObject()
    {
        return ingredientUIObject;
    }

    public IngredientType GetIngredientType()
    {
        return ingredientType;
    }
}