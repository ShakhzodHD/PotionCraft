﻿using System;
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
    public static int numberStands = 4;

    [SerializeField] private ProcessExchange exchange;
    [SerializeField] private SpawnerBuyers buyers;

    private BuyerPick buyerPick;
    private Animator animator;

    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float timeOfDestroying = 2f;

    private bool isProcessed = false;

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
        buyerPick = GetComponent<BuyerPick>();
        animator = GetComponent<Animator>();
        exchange = FindObjectOfType<ProcessExchange>();
        buyers = FindObjectOfType<SpawnerBuyers>();
    }

    private void Start()
    {
        InitializeNeeds();
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

        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * movementSpeed);
        }

        if (!animator.GetBool("isRunning"))
        {
            animator.SetBool("isRunning", true);
        }

        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= currentPath.Count)
            {
                StartCoroutine(WaitUntilItemPicked());
                animator.SetBool("isRunning", false);
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

        Array enumValues = Enum.GetValues(typeof(Resource.Potions));
        int needsCount = enumValues.Length - numberStands;

        possibleNeeds = new string[needsCount];

        for (int i = 0; i < needsCount; i++)
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
        buyers.CurrentBuyerCount--;
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