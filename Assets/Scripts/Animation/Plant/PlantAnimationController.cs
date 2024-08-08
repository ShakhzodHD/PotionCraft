using UnityEngine;

public class PlantAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerPickup[] playerPickups; 

    private Animator animator;
    private ResourceGenerator resourceGenerator;

    private int countTriggerRecovery = 0;
    private int countTriggerRemove = 2;

    private void Start()
    {
        animator = GetComponent<Animator>();
        resourceGenerator = GetComponent<ResourceGenerator>();

        resourceGenerator.SubscribeResourceGenerated(PlayAnimationRecovery);

        foreach (var playerPickup in playerPickups)
        {
            playerPickup.SubscribeResourceTaked(OnResourceTaked);
        }
    }

    private void OnDestroy()
    {
        resourceGenerator.UnsubscribeResourceGenerated(PlayAnimationRecovery);

        foreach (var playerPickup in playerPickups)
        {
            playerPickup.UnsubscribeResourceTaked(OnResourceTaked);
        }
    }

    private void OnResourceTaked(GameObject plant, PlayerPickup playerPickup)
    {
        if (plant == gameObject)
        {
            PlayAnimationRemove();
        }
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
