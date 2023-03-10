using UnityEngine;

public class CustomerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetBoolWalkAnimation(bool value)
    {
        animator.SetBool("IsWalking", value);
    }
}