using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnPointerClickEvent = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.Instance.IsIngredientButtonsInteractable) return;

        OnPointerClickEvent?.Invoke();
    }
}