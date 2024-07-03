using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopperAI : MonoBehaviour
{
    [Header ("Позиции точек передвижение")]
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private Vector3 pointExit;

    private string[] possibleNeeds;
    public string currentNeed;

    private ProcessExchange exchange;
    private BuyerPick buyerPick;

    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float timeFinish = 2f;
    [SerializeField] private float timeOfDestroying = 2f;

    private bool isProcessed = false;
    private float currentLerpTime;

    private State currentState;
    public enum State
    {
       Takeupable,
       Wearing,
       Buying,
       Return
    }
    private void Awake()
    {
        InitializeNeeds();

        buyerPick = GetComponent<BuyerPick>();
        exchange = FindObjectOfType<ProcessExchange>();
    }
    private void Start()
    {
        currentState = State.Takeupable;
        
        GenerateNeed();
    }
    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        if (isProcessed) return;

        Vector3 targetPosition = Vector3.zero;
        switch (currentState)
        {
            case State.Takeupable:
                targetPosition = pointA;
                break;
            case State.Wearing:
                if (buyerPick.isItemInPurchase == true)
                {
                    targetPosition = pointB;
                }
                else
                {
                    targetPosition = pointA;
                    return;
                }
                break;
            case State.Buying:
                if (exchange.isTradeable == true)
                {
                    currentState = State.Return;
                    currentLerpTime = 0f;
                    return;
                }
                else
                {
                    return;
                }
            case State.Return:
                exchange.isTradeable = false;
                targetPosition = pointExit;
                Invoke("DestroyObj", timeOfDestroying);
                break;
        }

        float distance = Vector3.Distance(transform.position, targetPosition);
        float step = movementSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition)
        {
            switch (currentState)
            {
                case State.Takeupable:
                    break;
                case State.Wearing:
                    break;
                case State.Buying:
                    break;
                case State.Return:
                    isProcessed = true;
                    break;
            }
            currentState = (State)(((int)currentState + 1) % System.Enum.GetValues(typeof(State)).Length);
        }
    }
    private void InitializeNeeds()
    {
        Array enumValues = Enum.GetValues(typeof(Resource.Potions));
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
}
