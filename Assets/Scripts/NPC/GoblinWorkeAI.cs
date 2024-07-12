using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GoblinWorkerAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private StorageManager storageManager; // Ссылка на компонент StorageManager

    // Возможные состояния гоблина
    private enum GoblinState
    {
        MovingToGenerator,
        MovingToCraft,
        MovingToStand
    }

    private GoblinState currentState; // Текущее состояние гоблина

    [SerializeField] private Transform generator;
    [SerializeField] private Transform craft;
    [SerializeField] private Transform stand;

    [SerializeField] private float waitTimeGenerator = 3f; // Время ожидания после достижения генератора
    [SerializeField] private float waitTimeCraft = 3f;     // Время ожидания после достижения крафта
    [SerializeField] private float waitTimeStand = 2f;     // Время ожидания после достижения стенда

    private bool isWaiting = false; // Флаг для проверки состояния ожидания
    private bool isCraft = false;   // Флаг для проверки выполнения задачи крафта

    [SerializeField] private bool needsCrystalForCraft = false;
    [SerializeField] private Transform crystalGenerator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        storageManager = stand.GetComponent<StorageManager>(); // Получаем компонент StorageManager у стенда

        // Начальное состояние - движение к генератору
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
    }
    private IEnumerator WaitAndMoveToNextTarget()
    {
        float waitTime = 0f;

        switch (currentState)
        {
            case GoblinState.MovingToGenerator:
                waitTime = waitTimeGenerator;
                yield return new WaitUntil(() => gameObject.transform.childCount > 0);
                break;
            case GoblinState.MovingToCraft:
                waitTime = waitTimeCraft;
                break;
            case GoblinState.MovingToStand:
                waitTime = waitTimeStand;
                // Ожидаем, пока количество дочерних объектов у стенда не станет меньше maxCount
                yield return new WaitUntil(() => stand.childCount < storageManager.maxCount);
                break;
        }

        // Ждем заданное время
        yield return new WaitForSeconds(waitTime);

        // Снимаем флаг ожидания
        isWaiting = false;

        // Переходим к следующей цели в зависимости от текущего состояния
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
