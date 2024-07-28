using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerPickup playerPickup;

    private Animator animator;
    private ResourceGenerator resourceGenerator;

    private int countTriggerRecovery = 0;
    private int countTriggerRemove = 2;
    private void Start()
    {
        animator = GetComponent<Animator>();
        resourceGenerator = GetComponent<ResourceGenerator>();

        resourceGenerator.SubscribeResourceGenerated(PlayAnimationRecovery);
        playerPickup.SubscribeResourceTaked(PlayAnimationRemove);
    }
    private void OnDestroy()
    {
        resourceGenerator.UnsubscribeResourceGenerated(PlayAnimationRecovery);
        playerPickup.UnsubscribeResourceTaked(PlayAnimationRemove);
    }
    private void PlayAnimationRecovery()
    {
        countTriggerRecovery++;
        countTriggerRemove--;

        if (countTriggerRecovery == 1)
        {
            animator.SetBool("IsFirstPart", true);
        }
        if (countTriggerRecovery == 2)
        {
            animator.SetBool("IsSecordPart", true);
        }
    }
    private void PlayAnimationRemove()
    {
        countTriggerRemove++;
        countTriggerRecovery--;

        if (countTriggerRemove == 1)
        {
            animator.SetBool("IsFirstPart", false);
        }
        if (countTriggerRemove == 2)
        {
            animator.SetBool("IsSecordPart", false);
        }
    }
}
