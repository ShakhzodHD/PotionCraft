using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopZone : MonoBehaviour
{
    [SerializeField] private int itemCost;
    [SerializeField] private int fullPrice;
    [SerializeField] GameObject itemToBuy;

    [SerializeField] private float framesPerUpdate = 3;
    private float frameCount = 0;
    private void OnTriggerStay(Collider other)
    {
        frameCount++;
        if (frameCount >= framesPerUpdate)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (CurrencyManager.instance.CanAfford(itemCost))
                {
                    CurrencyManager.instance.SpendCurrency(itemCost);
                    fullPrice--;
                }
                else
                {
                    Debug.Log("Not enough currency to buy the item!");
                }
            }
            frameCount = 0;
        }
    }
    private void Update()
    {
        BuyItem();
    }
    private void BuyItem()
    {
        if (fullPrice <= 0)
        {
            Instantiate(itemToBuy);
            Vector3 newPosition = gameObject.transform.position;
            newPosition.z += 2;
            itemToBuy.transform.position = newPosition;
            Destroy(gameObject);
        }
    }
}
