using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperAI : MonoBehaviour
{
    [Header("Позиции точек передвижения")]
    [SerializeField] private List<Vector3> pathToPointA;   
    [SerializeField] private List<Vector3> pathToPointB;  
    [SerializeField] private List<Vector3> pathToPointExit; 

    [Header("Позиции стендов с зельям")]
    [SerializeField] private Vector3 posStandRegen;
    [SerializeField] private Vector3 posStandPower;
    [SerializeField] private Vector3 posStandNecromancy;
    [SerializeField] private Vector3 posStandCurse;

    private string[] possibleNeeds;
    public string currentNeed;

    [SerializeField] private ProcessExchange exchange;
    private BuyerPick buyerPick;

    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float timeOfDestroying = 2f;

    private bool isProcessed = false;
    private bool isItemPicked = false;

    public enum State
    {
        Takeupable,
        Wearing,
        Buying,
        Return
    }

    private State currentState;
    private List<Vector3> currentPath;
    private int currentWaypointIndex;

    private void Awake()
    {
        InitializeNeeds();
        buyerPick = GetComponent<BuyerPick>();
    }

    private void Start()
    {
        currentState = State.Takeupable;
        GenerateNeed();
        MoveToNextState();
    }

    private void Update()
    {
        MoveAlongPath();
    }

    private void MoveToNextState()
    {
        if (isProcessed) return;

        switch (currentState)
        {
            case State.Takeupable:
                SetPathToPointA();
                currentWaypointIndex = 0;
                break;
            case State.Wearing:
                if (buyerPick.isItemInPurchase)
                {
                    currentPath = pathToPointB;
                    currentWaypointIndex = 0;
                }
                else
                {
                    SetPathToPointA();
                    currentWaypointIndex = 0;
                    currentState = State.Takeupable;
                }
                break;
            case State.Buying:
                StartCoroutine(CheckTradeableCoroutine());
                break;
            case State.Return:
                currentPath = pathToPointExit;
                currentWaypointIndex = 0;
                Invoke("DestroyObj", timeOfDestroying);
                break;
        }
    }

    private void SetPathToPointA()
    {
        currentPath = new List<Vector3>(pathToPointA);

        int needIndex = Array.IndexOf(possibleNeeds, currentNeed);

        switch (needIndex)
        {
            case 0:
                currentPath.Add(posStandRegen);
                break;
            case 1:
                currentPath.Add(posStandPower);
                break;
            case 2:
                currentPath.Add(posStandNecromancy);
                break;
            case 3:
                currentPath.Add(posStandCurse);
                break;
        }
    }

    private void MoveAlongPath()
    {
        if (isProcessed || currentPath == null || currentWaypointIndex >= currentPath.Count) return;

        Vector3 targetPosition = currentPath[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= currentPath.Count)
            {
                StartCoroutine(WaitUntilItemPicked());
            }
        }
    }

    private IEnumerator WaitUntilItemPicked()
    {
        yield return new WaitUntil(() => buyerPick.isItemInPurchase);
        OnReachedDestination();
    }
    private IEnumerator CheckTradeableCoroutine()
    {
        bool isCheckingTradeable = true;

        while (isCheckingTradeable)
        {
            yield return new WaitForSeconds(0.5f);

            if (exchange.isTradeable)
            {
                currentState = State.Return;
                MoveToNextState();
                isCheckingTradeable = false;
            }
        }
    }

    private void OnReachedDestination()
    {
        switch (currentState)
        {
            case State.Takeupable:
                currentState = State.Wearing;
                break;
            case State.Wearing:
                currentState = State.Buying;
                break;
            case State.Buying:
                currentState = State.Return;
                break;
            case State.Return:
                isProcessed = true;
                break;
        }

        MoveToNextState();
    }

    private void InitializeNeeds()
    {
        Array enumValues = Enum.GetValues(typeof(Resource.Potions)); // Подставьте свой собственный тип данных
        possibleNeeds = new string[enumValues.Length];
        for (int i = 0; i < enumValues.Length; i++)
        {
            possibleNeeds[i] = enumValues.GetValue(i).ToString();
        }
    }

    private void GenerateNeed()
    {
        int randomIndex = UnityEngine.Random.Range(0, possibleNeeds.Length);
        currentNeed = possibleNeeds[randomIndex];
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (pathToPointA != null)
        {
            foreach (var point in pathToPointA)
            {
                Gizmos.DrawSphere(point, 0.2f);
            }
        }

        Gizmos.color = Color.green;
        if (pathToPointB != null)
        {
            foreach (var point in pathToPointB)
            {
                Gizmos.DrawSphere(point, 0.2f);
            }
        }

        Gizmos.color = Color.blue;
        if (pathToPointExit != null)
        {
            foreach (var point in pathToPointExit)
            {
                Gizmos.DrawSphere(point, 0.2f);
            }
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(posStandRegen, 0.2f);
        Gizmos.DrawSphere(posStandPower, 0.2f);
        Gizmos.DrawSphere(posStandNecromancy, 0.2f);
        Gizmos.DrawSphere(posStandCurse, 0.2f);
    }
}
