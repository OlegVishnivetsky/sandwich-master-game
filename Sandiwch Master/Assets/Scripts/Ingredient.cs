using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private GameObject ingredientUIObject;
    [SerializeField] private IngredientType ingredientType;
    [Tooltip("Vertical offset from previous spawned ingredient")]
    [SerializeField] private float verticalOffset;

    public GameObject GetIngredientUIObject()
    {
        return ingredientUIObject;
    }

    public IngredientType GetIngredientType()
    {
        return ingredientType;
    }

    public float GetVerticalOffset()
    {
        return verticalOffset;
    }
}