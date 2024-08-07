using UnityEngine;

public class GoblinAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void TriggerAnim(bool isWork)
    {
        if (isWork == true)
        {
            StartAnimation();
        }
        else
        {
            StopAnimation();
        }
    }
    private void StartAnimation()
    {
        animator.SetBool("IsWork", true);
    }
    private void StopAnimation()
    {
        animator.SetBool("IsWork", false);
    }
}
