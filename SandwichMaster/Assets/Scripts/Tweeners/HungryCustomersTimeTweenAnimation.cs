using System.Collections;
using UnityEngine;

public class HungryCustomersTimeTweenAnimation : MonoBehaviour
{
    [SerializeField] private HungryCustomersTime hungryCustomersTime;
    [SerializeField] private TweenScaleAnimation tweenScaleAnimation;
    [SerializeField] private float showDuration;

    private void OnEnable()
    {
        hungryCustomersTime.OnHungryCustomersTimeStarted += ShowAndHideHungryCustomersTimeGameObject;
    }

    private void OnDisable()
    {
        hungryCustomersTime.OnHungryCustomersTimeStarted -= ShowAndHideHungryCustomersTimeGameObject;
    }

    public void ShowAndHideHungryCustomersTimeGameObject()
    {
        StartCoroutine(ShowAndHideHungryCustomersTimeGameObjectRoutine());
    }

    private IEnumerator ShowAndHideHungryCustomersTimeGameObjectRoutine()
    {
        tweenScaleAnimation.ScaleInAnimation(gameObject);
        yield return new WaitForSeconds(showDuration);
        tweenScaleAnimation.ScaleOutAnimation(gameObject);
    }
}