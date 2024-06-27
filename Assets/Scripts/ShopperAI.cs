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

    [SerializeField] private ProcessExchange exchange;
    [SerializeField] private BuyerPick buyerPick;

    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float timeFinish = 2f;
    [SerializeField] private float timeOfDestroying = 2f;

    private bool isProcessed = false;
    private float currentLerpTime;

    [SerializeField] private State currentState;
    public enum State
    {
       Takeupable,
       Wearing,
       Buying,
       Return
    }

    private void Start()
    {
        currentState = State.Takeupable;
        buyerPick = GetComponent<BuyerPick>();
        exchange = FindObjectOfType<ProcessExchange>();
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

    private void SearchProduct()
    {
        
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
