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
       Buying
       
    }

    private void Start()
    {
        currentState = State.Takeupable;
        buyerPick = GetComponent<BuyerPick>();
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
                    targetPosition = pointExit;                   
                }
                else
                {
                    targetPosition = pointB;
                    return;
                }
                break;
        }

        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= timeFinish)
        {
            switch (currentState)
            {
                case State.Takeupable:
                    break;
                case State.Wearing:
                    break;
                case State.Buying:
                    isProcessed = true;
                    break;
            }
            currentState = (State)(((int)currentState + 1) % System.Enum.GetValues(typeof(State)).Length);
            currentLerpTime = 0f; 
        }
        float t = currentLerpTime / timeFinish;
        transform.position = Vector3.Lerp(transform.position, targetPosition, t);
    }
}
