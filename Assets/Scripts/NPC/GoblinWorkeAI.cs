using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class GoblinWorkerAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private StorageManager storageManager; 
    private GoblinAnimationController goblinAnimator;
    private enum GoblinState
    {
        MovingToGenerator,
        MovingToCraft,
        MovingToStand
    }

    private GoblinState currentState; 

    [SerializeField] private Transform generator;
    [SerializeField] private Transform craft;
    [SerializeField] private Transform stand;

    [SerializeField] private float waitTimeGenerator = 3f; 
    [SerializeField] private float waitTimeCraft = 3f;     
    [SerializeField] private float waitTimeStand = 2f;     

    [SerializeField] private bool needsCrystalForCraft = false;
    [SerializeField] private Transform crystalGenerator;

    private bool isWaiting = false; 
    private bool isCraft = false;

    private int currentCountChild;

    private void Start()
    {
        currentCountChild = gameObject.transform.childCount;

        agent = GetComponent<NavMeshAgent>();
        storageManager = stand.GetComponent<StorageManager>(); 
        goblinAnimator = GetComponent<GoblinAnimationController>();

        currentState = GoblinState.MovingToGenerator;
        MoveToCurrentTarget();
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.2f && !isWaiting)
        {
            isWaiting = true;

            StartCoroutine(WaitAndMoveToNextTarget());
        }
        goblinAnimator.TriggerAnim(isWaiting);
    }
    private IEnumerator WaitAndMoveToNextTarget()
    {
        float waitTime = 0f;

        switch (currentState)
        {
            case GoblinState.MovingToGenerator:
                waitTime = waitTimeGenerator;
                yield return new WaitUntil(() => gameObject.transform.childCount > currentCountChild);
                break;
            case GoblinState.MovingToCraft:
                waitTime = waitTimeCraft;
                break;
            case GoblinState.MovingToStand:
                waitTime = waitTimeStand;
                yield return new WaitUntil(() => stand.childCount < storageManager.maxCount);
                break;
        }

        yield return new WaitForSeconds(waitTime);

        isWaiting = false;

        switch (currentState)
        {
            case GoblinState.MovingToGenerator:
                currentState = GoblinState.MovingToCraft;
                break;
            case GoblinState.MovingToCraft:
                if (!isCraft)
                {
                    currentState = GoblinState.MovingToGenerator;
                }
                else
                {
                    currentState = GoblinState.MovingToStand;
                    isCraft = false;
                }
                break;
            case GoblinState.MovingToStand:
                currentState = GoblinState.MovingToGenerator;
                break;
        }
        MoveToCurrentTarget();
    }

    private void MoveToCurrentTarget()
    {
        switch (currentState)
        {
            case GoblinState.MovingToGenerator:
                if (needsCrystalForCraft == true)
                {
                    agent.SetDestination(crystalGenerator.position);
                    needsCrystalForCraft = false;
                }
                else
                {
                    agent.SetDestination(generator.position);
                }
                break;
            case GoblinState.MovingToCraft:
                agent.SetDestination(craft.position);
                break;
            case GoblinState.MovingToStand:
                agent.SetDestination(stand.position);
                break;
        }
    }

    public void SetIsCraft(bool value)
    {
        isCraft = value;
    }
    public void SetNeedsCrystalForCraft(bool value)
    {
        needsCrystalForCraft = value;
    }
}
